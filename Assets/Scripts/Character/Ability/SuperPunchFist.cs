using UnityEngine;

public class SuperPunchFist : MonoBehaviour
{
    [SerializeField] 
    private Animator animator;
    public Animator Animator => animator;

    public void Play(string name) => animator.Play(name);

    public void Init(IHasAbility owner)
    {
        AbilityAnimationCallback callback = GetComponentInChildren<AbilityAnimationCallback>();
        callback.Init(owner);
    }
}
