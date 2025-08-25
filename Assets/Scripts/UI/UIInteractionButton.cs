using UnityEngine.UI;
using UnityEngine;

public class UIInteractionButton : MonoBehaviour
{
    [SerializeField]
    private Button button;

    private PlayerInteractor interactor;
    private IInteractable current;

    private void OnEnable()
    {
        InteractionEvents.OnCurrentChanged += HandleCurrentChanged;
        InteractionEvents.OnOpenUpgradePanel += (_,_) => button.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        InteractionEvents.OnCurrentChanged -= HandleCurrentChanged;
        InteractionEvents.OnOpenUpgradePanel -= (_,_) => button.gameObject.SetActive(false);
    }

    private void HandleCurrentChanged(IInteractable interactable)
    {
        current = interactable;
        button.gameObject.SetActive(current != null);
    }

    public void OnClick()
    {
        if (current == null) return;

        if (interactor == null)
        {
            interactor = GameManager.Instance.Player.GetComponent<PlayerInteractor>();
            if (interactor == null) return;
        }

        InteractionEvents.RaiseInteractRequested(current, interactor);
    }
}
