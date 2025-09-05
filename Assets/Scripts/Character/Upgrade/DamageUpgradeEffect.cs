using UnityEngine;

[CreateAssetMenu(menuName = "RoomSurvivor/Effects/DamageUpgradeEffect")]
public class DamageUpgradeEffect : UpgradeEffect
{
    public override void ApplyTo(GameObject target)
    {
        IHasAbility hasAbility = target.GetComponent<IHasAbility>();
        if (hasAbility != null)
        {
            CharacterStatus characterStatus = hasAbility.GetStatus() as CharacterStatus;
            if (characterStatus != null)
            {
                characterStatus.AttackDamageMultiplier += 0.1f;
            }
        }
    }
}
