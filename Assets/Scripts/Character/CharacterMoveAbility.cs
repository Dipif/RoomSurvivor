using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMoveAbility : AbilityBase
{
    Animator animator;
    public override void Initialize(IHasAbility owner)
    {
        base.Initialize(owner);

        animator = ((MonoBehaviour)owner).GetComponentInChildren<Animator>();
    }

    public override void Activate()
    {
        CharacterStatus status = (CharacterStatus)owner.GetStatus();
        if (status.IsMoveLock)
        {
            return;
        }
        if (status.MoveDirection == Vector3.zero)
        {
            return;
        }
        Vector3 next = ((MonoBehaviour)owner).transform.position 
            + status.MoveDirection * status.Speed * Time.fixedDeltaTime;

        ((MonoBehaviour)owner).transform.rotation = Quaternion.LookRotation(status.MoveDirection, Vector3.up);
        if (NavMesh.SamplePosition(next, out var hit, 1.0f, NavMesh.AllAreas))
            ((MonoBehaviour)owner).transform.position = hit.position;
        animator.SetBool("IsMoving", true);
    }

    public override void Deactivate()
    {
    }
}
