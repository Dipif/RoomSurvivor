using UnityEngine;

public class InteractionRouter : MonoBehaviour
{
    private void OnEnable()
    {
        InteractionEvents.OnInteracteRequested += HandleInteractRequested;
    }

    private void OnDisable()
    {
        InteractionEvents.OnInteracteRequested -= HandleInteractRequested;
    }

    private void HandleInteractRequested(IInteractable interactable, PlayerInteractor interactor)
    {
        interactable.Interact(interactor);
    }
}
