using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeOption", menuName = "Scriptable Objects/UpgradeOption")]
public class UpgradeOption : ScriptableObject
{
    public string Title;
    public string Description;
    public UpgradeEffect[] Effects;
}
