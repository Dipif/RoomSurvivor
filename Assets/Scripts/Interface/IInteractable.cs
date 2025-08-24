using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IInteractable
{
    void Interact(PlayerInteractor interactor);
}

public static class InteractionEvents
{
    public static System.Action<IInteractable> OnCurrentChanged;
    public static System.Action<IInteractable, PlayerInteractor> OnInteracteRequested;
    public static System.Action<List<UpgradeOption>, PlayerInteractor> OnOpenUpgradePanel;
    public static System.Action OnCloseUpgradePanel;

    public static void RaiseCurrentChanged(IInteractable interactable) => OnCurrentChanged?.Invoke(interactable);
    public static void RaiseInteractRequested(IInteractable interactable, PlayerInteractor interactor) => OnInteracteRequested?.Invoke(interactable, interactor);
    public static void RaiseOpenUpgradePanel(List<UpgradeOption> options, PlayerInteractor interactor) => OnOpenUpgradePanel?.Invoke(options, interactor);
    public static void RaiseCloseUpgradePanel() => OnCloseUpgradePanel?.Invoke();
}