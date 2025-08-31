using UnityEngine;

public class AbilityBase : MonoBehaviour
{
    [SerializeField]
    protected float baseCooldown = 0f;

    protected float cooldown = 0f;
    protected float remainingCooldown = 0f;

    [SerializeField]
    bool loop = false;

    protected GameObject owner;
    virtual public void Initialize(GameObject owner)
    {
        this.owner = owner;
        cooldown = baseCooldown;
    }

    private void FixedUpdate()
    {
        if (remainingCooldown > 0)
        {
            remainingCooldown -= Time.fixedDeltaTime;
            if (remainingCooldown < 0)
            {
                remainingCooldown = 0;
            }
        }
        if (loop && remainingCooldown == 0)
        {
            Activate();
            remainingCooldown = cooldown;
        }
    }
    virtual public bool CanActivate()
    {
        return remainingCooldown == 0;
    }
    virtual public void Activate() { }
    virtual public void Deactivate() { }
    virtual public void OnEvent(string eventName) { }
}
