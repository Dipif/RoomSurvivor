using UnityEngine;

public class CharacterStatus : StatusBase
{
    public float CurrentHealth = 100f;
    public float MaxHealth = 100f;
    public float AttackDamage = 10f;
    public float AttackDamageMultiplier = 1f;
    private float attackSpeedMultiplier = 1f;
    public float AttackSpeedMultiplier
    {
        get => attackSpeedMultiplier;
        set
        {
            if (attackSpeedMultiplier != value)
            {
                attackSpeedMultiplier = value;
                IHasAbility hasAbility = owner.GetComponent<IHasAbility>();
                hasAbility.OnAbilityEvent("ModifyAttackSpeed");
            }
        }
    }
    public float MoveSpeed = 5f;
    public float MoveSpeedMultiplier = 1f;
    
    public Vector3 MoveDirection { get; set; } = Vector3.zero;
    public bool canMove = true;
}
