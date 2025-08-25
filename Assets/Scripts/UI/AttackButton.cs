using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    [SerializeField]
    private Button button;

    private void OnEnable()
    {
        InteractionEvents.OnCurrentChanged += HandleCurrentChanged;
    }

    private void OnDisable()
    {
        InteractionEvents.OnCurrentChanged -= HandleCurrentChanged;
    }

    private void HandleCurrentChanged(IInteractable interactable)
    {
        button.gameObject.SetActive(interactable == null);
    }

    public void OnClick()
    {
        GameManager.Instance.Player.Attack();
    }
}
