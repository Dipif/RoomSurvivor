using UnityEngine;

public class EnemyStatus : StatusBase
{
    public float CurrentHealth = 100f;
    public float MaxHealth = 100f;
    public float AttackDamage = 10f;
    public float Speed = 5f;
    public float MoveLockTime = 0.5f; // Time to lock movement after an attack
    public float MoveLockTimer = 0f;
    public Vector3 MoveDirection = Vector3.zero;
    public bool IsMoveLock = false;
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0f)
        {
            Destroy(owner);
        }
    }
}
