using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public void OnClick()
    {
        GameManager.Instance.Player.Attack();
    }
}
