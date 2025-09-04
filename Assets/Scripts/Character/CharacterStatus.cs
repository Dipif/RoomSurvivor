using UnityEngine;

public class CharacterStatus : StatusBase
{
    public float CurrentHealth = 100f;
    public float MaxHealth = 100f;
    public float AttackDamage = 10f;
    public float Speed = 5f;
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
    public Vector3 MoveDirection { get; set; } = Vector3.zero;
}
