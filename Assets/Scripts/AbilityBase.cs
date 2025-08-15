using UnityEngine;

public class AbilityBase : MonoBehaviour
{
    protected GameObject owner;
    virtual public void Initialize(GameObject owner)
    {
        this.owner = owner;
    }
    virtual public void Activate() { }
    virtual public void Deactivate() { }
    virtual public void OnEvent(string eventName) { }
}
