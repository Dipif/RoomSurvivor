using UnityEngine;
using UnityEngine.AI;

public class CharacterMoveAbility : AbilityBase
{
    Animator animator;
    public override void Initialize(GameObject owner)
    {
        base.Initialize(owner);

        animator = owner.GetComponentInChildren<Animator>();
    }

    public override void Activate()
    {
        IHasAbility hasAbility = owner.GetComponent<IHasAbility>();
        CharacterStatus status = (CharacterStatus)hasAbility.GetStatus();
        if (status.IsMoveLock)
        {
            status.MoveLockTimer += Time.fixedDeltaTime;
            if (status.MoveLockTimer >= status.MoveLockTime)
            {
                status.IsMoveLock = false;
                status.MoveLockTimer = 0f;
            }
            return;
        }
        if (status.MoveDirection == Vector3.zero)
            return;

        Vector3 next = owner.transform.position 
            + status.MoveDirection * status.Speed * Time.fixedDeltaTime;

        Debug.DrawLine(owner.transform.position, next, Color.green, 0.1f);
        owner.transform.rotation = Quaternion.LookRotation(status.MoveDirection, Vector3.up);
        if (NavMesh.SamplePosition(next, out var hit, 1.0f, NavMesh.AllAreas))
            owner.transform.position = hit.position;
        animator.SetBool("IsMoving", true);
    }

    public override void Deactivate()
    {
        IHasAbility hasAbility = owner.GetComponent<IHasAbility>();
        CharacterStatus status = (CharacterStatus)hasAbility.GetStatus();
        status.MoveDirection = Vector3.zero;
        animator.SetBool("IsMoving", false);
    }
}
