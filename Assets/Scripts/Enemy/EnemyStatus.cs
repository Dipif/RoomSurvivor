using UnityEngine;

public class EnemyStatus : StatusBase
{
    public float CurrentHealth = 100f;
    public float MaxHealth = 100f;
    public float AttackDamage = 10f;
    public float Speed = 5f;
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0f)
        {
            Destroy(owner);
        }
    }
}
