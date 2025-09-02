using UnityEngine;

public class AbilityAnimationCallback : MonoBehaviour
{
    [SerializeField] 
    private MonoBehaviour hasAbilitySource;
    [SerializeField]
    IHasAbility hasAbility;
    void Start()
    {
        if (hasAbility == null)
            hasAbility = hasAbilitySource as IHasAbility;
    }

    public void Init(IHasAbility inHasAbility)
    {
        hasAbility = inHasAbility;
    }

    public void OnAbilityAnimationCallback(AnimationEvent e)
    {
        hasAbility.OnAbilityEvent(e.stringParameter);
    }
}
