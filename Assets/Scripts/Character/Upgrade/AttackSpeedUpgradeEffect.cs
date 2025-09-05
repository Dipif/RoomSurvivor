using UnityEngine;

[CreateAssetMenu(menuName = "RoomSurvivor/Effects/AttackSpeedUpgradeEffect")]
public class AttackSpeedUpgradeEffect : UpgradeEffect
{
    public override void ApplyTo(GameObject target)
    {
        IHasAbility hasAbility = target.GetComponent<IHasAbility>();
        if (hasAbility != null)
        {
            CharacterStatus characterStatus = hasAbility.GetStatus() as CharacterStatus;
            if (characterStatus != null)
            {
                characterStatus.AttackSpeedMultiplier += 0.1f;
            }
        }
    }
}
