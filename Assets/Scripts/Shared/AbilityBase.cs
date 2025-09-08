using UnityEngine;

public class AbilityBase : MonoBehaviour
{
    [SerializeField]
    protected float baseCooldown = 0f;

    protected float cooldown = 0f;
    protected float remainingCooldown = 0f;

    [SerializeField]
    protected bool loop = false;

    protected GameObject owner;
    public virtual void Initialize(GameObject owner)
    {
        this.owner = owner;
        cooldown = baseCooldown;
    }

    protected virtual void FixedUpdate()
    {
        if (remainingCooldown > 0)
        {
            remainingCooldown -= Time.fixedDeltaTime;
            if (remainingCooldown < 0)
            {
                remainingCooldown = 0;
            }
        }
        if (loop && remainingCooldown == 0 && CanActivate())
        {
            Activate();
            remainingCooldown = cooldown;
        }
    }
    public virtual bool CanActivate()
    {
        return true;
    }
    public virtual void Activate() { }
    public virtual void Deactivate() { }
    public virtual void OnEvent(string eventName) { }
}
