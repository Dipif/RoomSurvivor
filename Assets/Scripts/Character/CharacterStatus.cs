using System;
using UnityEngine;

public class CharacterStatus : StatusBase
{
    public event Action<float, float> OnHealthChanged; // (current, max)
    public event Action OnDied;

    [SerializeField] private float currentHealth = 3f;
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackDamageMultiplier = 1f;
    [SerializeField] private float attackSpeedMultiplier = 1f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float moveSpeedMultiplier = 1f;

    public float CurrentHealth
    {
        get => currentHealth;
        private set
        {
            var clamped = Mathf.Clamp(value, 0, MaxHealth);
            if (Mathf.Approximately(clamped, currentHealth)) return;
            currentHealth = clamped;
            OnHealthChanged?.Invoke(currentHealth, MaxHealth);
            if (currentHealth <= 0f) OnDied?.Invoke();
        }
    }
    public float MaxHealth
    {
        get => maxHealth;
        set
        {
            if (Mathf.Approximately(maxHealth, value)) return;
            maxHealth = Mathf.Max(1f, value);
            OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        }
    }
    public float AttackDamage
    {
        get => attackDamage;
        set
        {
            if (Mathf.Approximately(attackDamage, value)) return;
            attackDamage = value;
        }
    }
    public float AttackDamageMultiplier
    {
        get => attackDamageMultiplier;
        set
        {
            if (Mathf.Approximately(attackDamageMultiplier, value)) return;
            attackDamageMultiplier = value;
        }
    }
    public float AttackSpeedMultiplier
    {
        get => attackSpeedMultiplier;
        set
        {
            if (Mathf.Approximately(attackSpeedMultiplier, value)) return;
            attackSpeedMultiplier = value;
            owner.GetComponent<IHasAbility>()?.OnAbilityEvent("ModifyAttackSpeed");
        }
    }
    public float MoveSpeed
    {
        get => moveSpeed;
        set
        {
            if (Mathf.Approximately(moveSpeed, value)) return;
            moveSpeed = value;
        }
    }
    public float MoveSpeedMultiplier
    {
        get => moveSpeedMultiplier;
        set
        {
            if (Mathf.Approximately(moveSpeedMultiplier, value)) return;
            moveSpeedMultiplier = value;
        }
    }


    public override void ResetStatus()
    {
        CurrentHealth = MaxHealth;
        AttackDamageMultiplier = 1f;
        AttackSpeedMultiplier = 1f;
        MoveSpeedMultiplier = 1f;
        canMove = true;
        MoveDirection = Vector3.zero;
    }

    public Vector3 MoveDirection { get; set; } = Vector3.zero;
    public bool canMove = true;

    public void TakeDamage(float damage)
    {
        if (damage <= 0f) return;
        CurrentHealth -= damage; // setter가 이벤트를 쏨
        if (CurrentHealth <= 0f)
        {
            OnDied?.Invoke();
        }
    }

    public void Heal(float amount)
    {
        if (amount <= 0f) return;
        CurrentHealth += amount;
    }

    public void ForceSync() => OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
}
