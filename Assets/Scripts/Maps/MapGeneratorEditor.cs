#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var gen = (MapGenerator)target;

        GUILayout.Space(8);
        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Preview Generate"))
            {
                gen.RebuildAll();   // ← 직접 호출
                GUIUtility.ExitGUI();    // ← GUI 루프 종료(Assert 예방)
            }
            if (GUILayout.Button("Clear Preview"))
            {
                gen.Clear();          // ← 직접 호출 (public이어야 함)
                GUIUtility.ExitGUI();
            }
        }
    }
}


[InitializeOnLoad]
static class MapPreviewAutoClear
{
    static MapPreviewAutoClear()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        // 에디터 -> 플레이로 넘어가기 직전
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            foreach (var gen in Object.FindObjectsByType<MapGenerator>(FindObjectsSortMode.None))
            {
                gen.Clear();
            }
        }
    }
}
#endif
