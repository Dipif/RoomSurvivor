using UnityEngine;

public class CharacterAnimationCallback : MonoBehaviour
{
    [SerializeField]
    Character Character;
    public void OnAttackPerform()
    {
        Character.OnAttackPerform();
    }
}
