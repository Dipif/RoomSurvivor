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
                gen.RebuildInEditor();   // �� ���� ȣ��
                GUIUtility.ExitGUI();    // �� GUI ���� ����(Assert ����)
            }
            if (GUILayout.Button("Clear Preview"))
            {
                gen.Clear();          // �� ���� ȣ�� (public�̾�� ��)
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
        // ������ -> �÷��̷� �Ѿ�� ����
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
