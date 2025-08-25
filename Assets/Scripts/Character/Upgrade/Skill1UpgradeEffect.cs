using UnityEngine;

[CreateAssetMenu(menuName = "RoomSurvivor/Effects/Skill1Upgrade")]
public class Skill1UpgradeEffect : UpgradeEffect
{
    public override void ApplyTo(GameObject target)
    {
        Debug.Log("Skill1UpgradeEffect applied to " + target.name);
    }
}
