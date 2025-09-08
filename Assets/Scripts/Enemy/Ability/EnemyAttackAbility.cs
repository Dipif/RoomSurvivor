using UnityEngine;

public class EnemyAttackAbility : AbilityBase
{

    public override void Activate()
    {
        GameManager.Instance.Player.gameObject.GetComponent<KnockbackController>().ApplyFromSource(owner.transform.position, 1.0f, 0.2f);
    }

    public override bool CanActivate()
    {
        Vector2 playerPos = GameManager.Instance.Player.transform.position;
        Vector2 enemyPos = owner.transform.position;
        float distance = Vector2.Distance(playerPos, enemyPos);
        return distance <= 0.5f;
    }
}
