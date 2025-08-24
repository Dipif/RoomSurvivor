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
        if (interactable == null)
        {
            button.gameObject.SetActive(true);
        }
        button.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        GameManager.Instance.Player.Attack();
    }
}
