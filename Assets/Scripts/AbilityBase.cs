using UnityEngine;

public class AbilityBase : MonoBehaviour
{
    protected IHasAbility owner;
    virtual public void Initialize(IHasAbility owner)
    {
        this.owner = owner;
    }
    virtual public void Activate() { }
    virtual public void Deactivate() { }
}
