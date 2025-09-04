using UnityEngine;

[CreateAssetMenu(menuName = "RoomSurvivor/Effects/SuperPunchDamageUpgradeEffect")]
public class SuperPunchDamageUpgradeEffect : UpgradeEffect
{
    public override void ApplyTo(GameObject target)
    {
        CharacterStatus status = target.GetComponent<CharacterStatus>();
        status.SuperPunchDamageMultiplier += 0.5f;
    }
}
