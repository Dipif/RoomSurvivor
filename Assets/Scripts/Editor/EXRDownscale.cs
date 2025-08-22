// Assets/Editor/EXRDownscale1024.cs
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public static class EXRDownscale1024
{
    [MenuItem("Tools/Textures/Downscale Selected EXR To 1024")]
    public static void Run()
    {
        var paths = CollectSelectedEXRPaths();
        if (paths.Count == 0) { Debug.LogWarning("선택된 EXR이 없습니다."); return; }

        int ok = 0, skip = 0;
        AssetDatabase.StartAssetEditing();
        try
        {
            foreach (var p in paths)
            {
                if (Process(p)) ok++; else skip++;
            }
        }
        finally
        {
            AssetDatabase.StopAssetEditing();
            AssetDatabase.Refresh();
        }
        Debug.Log($"EXR 1024 변환 완료 — 처리 {ok}개, 스킵 {skip}개");
    }

    static List<string> CollectSelectedEXRPaths()
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
                    if (Path.GetExtension(p).ToLowerInvariant() == ".exr") list.Add(p);
                }
            }
            else
            {
                if (Path.GetExtension(path).ToLowerInvariant() == ".exr") list.Add(path);
            }
        }
        return list.Distinct().ToList();
    }

    static bool Process(string path)
    {
        var importer = AssetImporter.GetAtPath(path) as TextureImporter;
        if (importer == null) return false;

        var tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        if (tex == null) { Debug.LogWarning($"로드 실패: {path}"); return false; }

        // 이미 1024x1024면 스킵
        if (tex.width == 1024 && tex.height == 1024) return false;

        // HDR 유지용: ARGBHalf RT → RGBAHalf Texture2D
        var rt = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBHalf);
        rt.Create();
        var prevActive = RenderTexture.active;

        Graphics.Blit(tex, rt);
        RenderTexture.active = rt;

        var outTex = new Texture2D(1024, 1024, TextureFormat.RGBAHalf, false, true); // linear
        outTex.ReadPixels(new Rect(0, 0, 1024, 1024), 0, 0);
        outTex.Apply(false, false);

        RenderTexture.active = prevActive;
        rt.Release();

        // EXR(half-float)로 덮어쓰기
        byte[] bytes = ImageConversion.EncodeToEXR(outTex);
        // 압축 원하면 ZIP 사용:
        // byte[] bytes = ImageConversion.EncodeToEXR(outTex, Texture2D.EXRFlags.CompressZIP);
        Object.DestroyImmediate(outTex);
        File.WriteAllBytes(path, bytes);

        // 임포터 오버라이드 (데이터/HDR 맵 전제)
        importer.sRGBTexture = false; // 선형
        importer.maxTextureSize = 1024;
        importer.textureCompression = TextureImporterCompression.Uncompressed; // 정밀도 보존
        importer.SaveAndReimport();

        return true;
    }
}
