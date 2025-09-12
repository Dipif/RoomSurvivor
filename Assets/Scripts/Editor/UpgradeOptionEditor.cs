using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UpgradeOption))]
public class UpgradeOptionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SerializedProperty costType = serializedObject.FindProperty("costType");
        SerializedProperty manualCosts = serializedObject.FindProperty("manualCosts");
        SerializedProperty linearStartCost = serializedObject.FindProperty("linearStartCost");
        SerializedProperty linearIncrement = serializedObject.FindProperty("linearIncrement");
        serializedObject.Update();

        EditorGUILayout.PropertyField(costType);
        if (costType.enumValueIndex == (int)CostType.Manual)
        {
            EditorGUILayout.PropertyField(manualCosts);
        }
        else if (costType.enumValueIndex == (int)CostType.Linear)
        {
            EditorGUILayout.PropertyField(linearStartCost);
            EditorGUILayout.PropertyField(linearIncrement);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
