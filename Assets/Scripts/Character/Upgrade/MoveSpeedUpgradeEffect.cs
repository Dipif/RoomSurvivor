using UnityEngine;

[CreateAssetMenu(menuName = "RoomSurvivor/Effects/MoveSpeedUpgradeEffect")]
public class MoveSpeedUpgradeEffect : UpgradeEffect
{
    public override void ApplyTo(GameObject target)
    {
        Debug.Log("MoveSpeedUpgradeEffect applied to " + target.name);
    }
}
