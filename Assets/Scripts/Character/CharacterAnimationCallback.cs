using UnityEngine;

public class CharacterAnimationCallback : MonoBehaviour
{
    [SerializeField]
    Character Character;
    public void OnAbilityAnimationCallback(AnimationEvent e)
    {
        Character.OnAbilityAnimationCallback(e.stringParameter);
    }
}
