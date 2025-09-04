using UnityEngine;

[CreateAssetMenu(menuName = "RoomSurvivor/Effects/ComboPunchUpgradeEffect")]
public class ComboPunchUpgradeEffect : UpgradeEffect
{
    public override void ApplyTo(GameObject target)
    {
        Debug.Log("ComboPunchUpgradeEffect applied to " + target.name);
    }
}
