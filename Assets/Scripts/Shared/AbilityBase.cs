using UnityEngine;

public class AbilityBase : MonoBehaviour
{
    [SerializeField]
    protected float baseCooldown = 0f;

    protected float cooldown = 0f;
    protected float remainingCooldown = 0f;

    [SerializeField]
    protected bool loop = false;

    [SerializeField]
    protected bool hasDuration = false;
    [SerializeField]
    protected float duration = 0f;

    protected float remainingDuration = 0f;

    protected GameObject owner;
    public virtual void Initialize(GameObject owner)
    {
        this.owner = owner;
        cooldown = baseCooldown;
        remainingCooldown = cooldown;
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
            if (hasDuration)
            {
                remainingDuration = duration;
            }
        }
        if (hasDuration && remainingDuration > 0)
        {
            remainingDuration -= Time.fixedDeltaTime;
            if (remainingDuration <= 0)
            {
                Deactivate();
                remainingDuration = 0;
            }
        }
    }
    public virtual bool CanActivate()
    {
        return true;
    }
    public virtual void Activate() 
    { 
        if (hasDuration)
        {
            remainingDuration = duration;
        }
    }
    public virtual void Deactivate() { }
    public virtual void OnEvent(string eventName) { }
}
