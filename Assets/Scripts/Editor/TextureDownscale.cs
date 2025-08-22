// Assets/Editor/TextureDownscale1024_NormalSafe.cs
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public static class TextureDownscale1024
{
    [MenuItem("Tools/Textures/Downscale Selected PNGs To 1024 (Normal-safe)")]
    public static void Run()
    {
        var paths = CollectSelectedPngPaths();
        if (paths.Count == 0) { Debug.LogWarning("선택된 PNG 텍스처가 없습니다."); return; }

        int ok = 0, skip = 0;
        AssetDatabase.StartAssetEditing();
        try
        {
            foreach (var p in paths)
            {
                if (ProcessOne(p)) ok++; else skip++;
            }
        }
        finally
        {
            AssetDatabase.StopAssetEditing();
            AssetDatabase.Refresh();
        }
        Debug.Log($"1024 변환 완료 — 처리 {ok}개, 스킵 {skip}개");
    }

    static List<string> CollectSelectedPngPaths()
    {
        var list = new List<string>();
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
                    if (Path.GetExtension(p).ToLowerInvariant() == ".png") list.Add(p);
                }
            }
            else
            {
                if (Path.GetExtension(path).ToLowerInvariant() == ".png") list.Add(path);
            }
        }
        return list.Distinct().ToList();
    }

    static bool ProcessOne(string path)
    {
        var importer = AssetImporter.GetAtPath(path) as TextureImporter;
        if (importer == null) return false;

        // 원래 임포터 세팅 백업
        var bak = new TextureImporterSettings();
        importer.ReadTextureSettings(bak);
        bool wasNormal = importer.textureType == TextureImporterType.NormalMap;

        // 리샘플을 "정확한 원본 RGBA"로 하기 위해, 노말맵이면 잠시 Default/선형으로 재임포트
        if (wasNormal)
        {
            importer.textureType = TextureImporterType.Default;
            importer.sRGBTexture = false;                  // 데이터맵은 선형
            importer.alphaSource = TextureImporterAlphaSource.FromInput;
            importer.npotScale = TextureImporterNPOTScale.None;
            importer.SaveAndReimport();
        }

        var tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        if (tex == null) { Debug.LogWarning($"로드 실패: {path}"); return false; }

        // 이미 1024×1024면 스킵
        if (tex.width == 1024 && tex.height == 1024)
        {
            // 원래 노말맵이었다면 임포터만 복원
            if (wasNormal) RestoreNormalImporter(importer);
            return false;
        }

        // 1024x1024 강제 리샘플 (선형로 처리)
        var rt = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGB32);
        var prevActive = RenderTexture.active;

        // 블릿 시 비선형 보정 피하려면 소스는 선형 상태가 안전
        var prevFilter = tex.filterMode; tex.filterMode = FilterMode.Bilinear;
        Graphics.Blit(tex, rt);
        RenderTexture.active = rt;

        var outTex = new Texture2D(1024, 1024, TextureFormat.RGBA32, false, true); // linear
        outTex.ReadPixels(new Rect(0, 0, 1024, 1024), 0, 0);
        outTex.Apply(false, false);

        // 복구
        tex.filterMode = prevFilter;
        RenderTexture.active = prevActive;
        rt.Release();

        // PNG 덮어쓰기
        File.WriteAllBytes(path, outTex.EncodeToPNG());
        Object.DestroyImmediate(outTex);

        // 임포터 설정 적용
        if (wasNormal)
        {
            RestoreNormalImporter(importer); // NormalMap로 복원(선형)
        }
        else
        {
            // 일반 텍스처: 기존 타입 유지, 단 업스케일 방지용 max size 고정
            importer.maxTextureSize = 1024;
            importer.SaveAndReimport();
        }

        return true;
    }

    static void RestoreNormalImporter(TextureImporter importer)
    {
        importer.textureType = TextureImporterType.NormalMap;
        importer.convertToNormalmap = false;       // 원본이 이미 노말맵이라고 가정
        importer.sRGBTexture = false;              // 노말은 선형
        importer.maxTextureSize = 1024;
        importer.textureCompression = TextureImporterCompression.Uncompressed; // 필요 시 압축 옵션 조정
        importer.normalmapFilter = TextureImporterNormalFilter.Standard;
        importer.SaveAndReimport();
    }
}
