using UnityEngine;

public class EnemyStatus : StatusBase
{
    public float CurrentHealth = 100f;
    public float MaxHealth = 100f;
    public float AttackDamage = 10f;
    public float Speed = 5f;
    public override void Initialize(GameObject owner)
    {
        base.Initialize(owner);
        CurrentHealth = MaxHealth;
    }
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        GameManager.Instance.DamageTextPool.ShowText(owner.transform.position + Vector3.forward, damage.ToString(), FloatingTextType.Damage);
        if (CurrentHealth <= 0f)
        {
            owner.GetComponent<Enemy>().OnDied();
        }
    }
}
