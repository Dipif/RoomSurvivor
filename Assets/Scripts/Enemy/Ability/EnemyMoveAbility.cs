using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveAbility : AbilityBase
{
    [SerializeField]
    NavMeshAgent agent;

    [SerializeField]
    Animator animator;

    [SerializeField]
    float stoppingDistance = 1.5f;
    public override void Initialize(GameObject owner)
    {
        base.Initialize(owner);

        if (agent == null)
            Debug.LogError("NavMeshAgent is not assigned in EnemyMoveAbility");
        if (animator == null)
            Debug.LogError("Animator is not assigned in EnemyMoveAbility");
    }

    public override void Activate()
    {
        if (!CanActivate()) return;
        GameObject target = GameManager.Instance.Player.gameObject;
        agent.stoppingDistance = stoppingDistance;
        float distance = Vector3.Distance(owner.transform.position, target.transform.position);
        if (distance <= stoppingDistance)
        {
            agent.isStopped = true;
            animator.SetBool("IsMoving", false);
            return;
        }
        agent.isStopped = false;
        agent.SetDestination(target.transform.position);
        animator.SetBool("IsMoving", true);
    }

    public override void Deactivate()
    {
        agent.ResetPath();
        animator.SetBool("IsMoving", false);
    }
}
