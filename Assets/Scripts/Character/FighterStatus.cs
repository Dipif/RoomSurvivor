using UnityEngine;

public class FighterStatus : CharacterStatus
{
    public float SuperPunchDamageMultiplier = 1f;
    private float superPunchCooldownMultiplier = 1f;
    public float SuperPunchCooldownMultiplier
    {
        get => superPunchCooldownMultiplier;
        set
        {
            if (superPunchCooldownMultiplier != value)
            {
                superPunchCooldownMultiplier = value;
                IHasAbility hasAbility = owner.GetComponent<IHasAbility>();
                hasAbility.OnAbilityEvent("ModifySuperPunchCooldown");
            }
        }
    }
}
