using UnityEngine;

[CreateAssetMenu(menuName = "RoomSurvivor/Effects/SuperPunchCooldownUpgradeEffect")]
public class SuperPunchCooldownUpgradeEffect : UpgradeEffect
{
    public override void ApplyTo(GameObject target)
    {
        CharacterStatus status = target.GetComponent<CharacterStatus>();
        status.SuperPunchCooldownMultiplier -= 0.1f;
    }
}
