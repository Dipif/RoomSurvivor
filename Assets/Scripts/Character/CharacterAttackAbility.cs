using System.Net.NetworkInformation;
using UnityEngine;

public class CharacterAttackAbility : AbilityBase
{
    Animator animator;

    public override void Initialize(IHasAbility owner)
    {
        base.Initialize(owner);

        animator = ((MonoBehaviour)owner).GetComponentInChildren<Animator>();
    }

    public override void Activate()
    {
        animator.SetBool("IsMoving", false);
        CharacterStatus status = (CharacterStatus)owner.GetStatus();
        status.MoveLockTimer = 0f;
        status.IsMoveLock = true;
        animator.Play("Attack");
    }

    public override void Deactivate()
    {
        CharacterStatus status = (CharacterStatus)owner.GetStatus();
        status.IsMoveLock = false;
        status.MoveLockTimer = 0f;
        animator.SetBool("IsMoving", false);
        animator.Play("Idle");
    }
}
