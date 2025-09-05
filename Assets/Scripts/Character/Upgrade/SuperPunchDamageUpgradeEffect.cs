using UnityEngine;

[CreateAssetMenu(menuName = "RoomSurvivor/Effects/SuperPunchDamageUpgradeEffect")]
public class SuperPunchDamageUpgradeEffect : UpgradeEffect
{
    public override void ApplyTo(GameObject target)
    {
        FighterStatus status = target.GetComponent<FighterStatus>();
        status.SuperPunchDamageMultiplier += 0.5f;
    }
}
