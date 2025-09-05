using UnityEngine;

[CreateAssetMenu(menuName = "RoomSurvivor/Effects/MoveSpeedUpgradeEffect")]
public class MoveSpeedUpgradeEffect : UpgradeEffect
{
    public override void ApplyTo(GameObject target)
    {
        IHasAbility hasAbility = target.GetComponent<IHasAbility>();
        if (hasAbility != null)
        {
            CharacterStatus characterStatus = hasAbility.GetStatus() as CharacterStatus;
            if (characterStatus != null)
            {
                characterStatus.MoveSpeedMultiplier += 0.1f;
            }
        }
    }
}
