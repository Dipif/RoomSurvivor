using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeOption", menuName = "ScriptableObjects/UpgradeOption")]
public class UpgradeOption : ScriptableObject
{
    public string Title;
    public string Description;
    public UpgradeEffect[] Effects;
}
