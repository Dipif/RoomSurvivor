using UnityEngine;

[CreateAssetMenu(menuName = "RoomSurvivor/Effects/SuperPunchCooldownUpgradeEffect")]
public class SuperPunchCooldownUpgradeEffect : UpgradeEffect
{
    public override void ApplyTo(GameObject target)
    {
        FighterStatus status = target.GetComponent<FighterStatus>();
        status.SuperPunchCooldownMultiplier -= 0.1f;
    }
}
