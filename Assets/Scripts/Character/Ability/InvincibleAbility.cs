using UnityEngine;
using UnityEngine.AI;

public class InvincibleAbility : AbilityBase
{
    public override void Activate()
    {
        base.Activate();
        IHasAbility hasAbility = owner.GetComponent<IHasAbility>();
        CharacterStatus status = (CharacterStatus)hasAbility.GetStatus();
        status.IsInvincible = true;
    }
    public override void Deactivate()
    {
        IHasAbility hasAbility = owner.GetComponent<IHasAbility>();
        CharacterStatus status = (CharacterStatus)hasAbility.GetStatus();
        status.IsInvincible = false;
    }
}
