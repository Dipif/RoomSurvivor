// Assets/Editor/TextureDownscale1024Simple.cs
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public static class TextureDownscale1024Simple
{
    [MenuItem("Tools/Textures/Downscale Selected PNGs To 1024")]
    public static void DownscaleSelectedPngsTo1024()
    {
        var pngPaths = CollectSelectedPngPaths();
        if (pngPaths.Count == 0)
        {
            Debug.LogWarning("선택된 PNG 텍스처가 없습니다.");
            return;
        }

        int processed = 0, skipped = 0;
        AssetDatabase.StartAssetEditing();
        try
        {
            foreach (var path in pngPaths)
            {
                if (ProcessOne(path)) processed++;
                else skipped++;
            }
        }
        finally
        {
            AssetDatabase.StopAssetEditing();
            AssetDatabase.Refresh();
        }
        Debug.Log($"PNG 1024 변환 완료 — 처리 {processed}개, 스킵 {skipped}개");
    }
    static bool IsDataMap(string path)
    {
        var n = System.IO.Path.GetFileNameWithoutExtension(path).ToLowerInvariant();
        return n.Contains("_ao") || n.Contains("_metal") || n.Contains("_rough")
            || n.Contains("_mask") || n.Contains("_height") || n.Contains("_disp");
    }

    static List<string> CollectSelectedPngPaths()
    {
        var result = new List<string>();
        foreach (var obj in Selection.objects)
        {
            var path = AssetDatabase.GetAssetPath(obj);
            if (string.IsNullOrEmpty(path)) continue;

            if (AssetDatabase.IsValidFolder(path))
            {
                var guids = AssetDatabase.FindAssets("t:Texture2D", new[] { path });
                foreach (var g in guids)
                {
                    var p = AssetDatabase.GUIDToAssetPath(g);
                    if (Path.GetExtension(p).ToLowerInvariant() == ".png")
                        result.Add(p);
                }
            }
            else
            {
                if (Path.GetExtension(path).ToLowerInvariant() == ".png")
                    result.Add(path);
            }
        }
        return result.Distinct().ToList();
    }

    static bool ProcessOne(string path)
    {
        var importer = AssetImporter.GetAtPath(path) as TextureImporter;
        if (importer == null) return false;

        if (importer.textureType != TextureImporterType.NormalMap && IsDataMap(path))
        {
            importer.sRGBTexture = false; // 데이터 맵은 선형
        }

        var tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        if (tex == null) { Debug.LogWarning($"로드 실패: {path}"); return false; }

        // 이미 1024×1024면 스킵
        if (tex.width == 1024 && tex.height == 1024)
            return false;

        // 노말맵/선형 판단 (출력 텍스처 생성 시 사용)
        bool outputLinear = (importer.textureType == TextureImporterType.NormalMap) || !importer.sRGBTexture;

        // 1024x1024로 강제 리샘플
        var rt = RenderTexture.GetTemporary(1024, 1024, 0, RenderTextureFormat.ARGB32);
        var prevActive = RenderTexture.active;

        var prevFilter = tex.filterMode;
        tex.filterMode = FilterMode.Bilinear;

        Graphics.Blit(tex, rt);
        RenderTexture.active = rt;

        var outTex = new Texture2D(1024, 1024, TextureFormat.RGBA32, false, outputLinear);
        outTex.ReadPixels(new Rect(0, 0, 1024, 1024), 0, 0);
        outTex.Apply(false, false);

        // 복구
        tex.filterMode = prevFilter;
        RenderTexture.active = prevActive;
        RenderTexture.ReleaseTemporary(rt);

        // PNG로 덮어쓰기
        byte[] bytes = outTex.EncodeToPNG();
        Object.DestroyImmediate(outTex);
        File.WriteAllBytes(path, bytes);

        // 임포터 설정 오버라이드
        importer.maxTextureSize = 1024;

        if (importer.textureType == TextureImporterType.NormalMap)
        {
            importer.textureType = TextureImporterType.NormalMap;
            importer.sRGBTexture = false; // 노말맵은 선형
            importer.convertToNormalmap = false; // 기존 노말맵 그대로 사용
        }
        // 그 외 타입은 변경하지 않음(스프라이트/디폴트 등 유지)

        importer.isReadable = false; // 기본적으로 읽기 비활성
        importer.SaveAndReimport();

        return true;
    }
}
