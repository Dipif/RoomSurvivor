using UnityEngine;

[CreateAssetMenu(menuName = "RoomSurvivor/Effects/Skill2Upgrade")]
public class Skill2UpgradeEffect : UpgradeEffect
{
    public override void ApplyTo(GameObject target)
    {
        Debug.Log("Skill2UpgradeEffect applied to " + target.name);
    }
}
