using UnityEngine;

[CreateAssetMenu(menuName = "RoomSurvivor/Effects/DamageUpgradeEffect")]
public class DamageUpgradeEffect : UpgradeEffect
{
    public override void ApplyTo(GameObject target)
    {
        Debug.Log("DamageUpgradeEffect applied to " + target.name);
    }
}
