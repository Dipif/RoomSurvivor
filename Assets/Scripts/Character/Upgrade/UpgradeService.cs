using UnityEngine;

public static class UpgradeService
{
    public static void ApplyTo(GameObject target, UpgradeOption option)
    {
        foreach (var effect in option.Effects)
        {
            effect.ApplyTo(target);
        }
    }
}
