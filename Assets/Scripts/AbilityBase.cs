using UnityEngine;

public class AbilityBase : MonoBehaviour
{
    [SerializeField]
    protected float Cooldown = 0f;
    protected float RemainingCooldown = 0f;
    protected GameObject owner;
    virtual public void Initialize(GameObject owner)
    {
        this.owner = owner;
    }
    virtual public void Activate() { }
    virtual public void Deactivate() { }
    virtual public void OnEvent(string eventName) { }
}
