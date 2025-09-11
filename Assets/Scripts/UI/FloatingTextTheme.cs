using System;
using System.Collections.Generic;
using UnityEngine;

public enum FloatingTextType
{
    Damage,
    Gold,
}

[CreateAssetMenu(fileName = "FloatingTextTheme", menuName = "Scriptable Objects/FloatingTextTheme")]
public class FloatingTextTheme : ScriptableObject
{
    [Serializable]
    public struct StyleEntry
    {
        public FloatingTextType type;
        public FloatingTextStyle style;
    }

    public List<StyleEntry> styles = new List<StyleEntry>();
    private Dictionary<FloatingTextType, FloatingTextStyle> styleDict;

    private void OnEnable()
    {
        styleDict = new Dictionary<FloatingTextType, FloatingTextStyle>();
        foreach (var entry in styles)
        {
            if (!styleDict.ContainsKey(entry.type))
            {
                styleDict.Add(entry.type, entry.style);
            }
        }
    }

    public FloatingTextStyle GetStyle(FloatingTextType type)
    {
        if (styleDict.TryGetValue(type, out var style))
        {
            return style;
        }
        Debug.LogWarning($"No style found for FloatingTextType {type}");
        return null;
    }
}
