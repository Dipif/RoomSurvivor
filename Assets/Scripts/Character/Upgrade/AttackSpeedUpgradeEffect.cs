using UnityEngine;

[CreateAssetMenu(menuName = "RoomSurvivor/Effects/AttackSpeedUpgradeEffect")]
public class AttackSpeedUpgradeEffect : UpgradeEffect
{
    public override void ApplyTo(GameObject target)
    {
        Debug.Log("AttackSpeedUpgradeEffect applied to " + target.name);
    }
}
