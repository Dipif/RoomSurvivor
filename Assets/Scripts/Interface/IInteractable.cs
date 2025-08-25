using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IInteractable
{
    void Interact(PlayerInteractor interactor);
}

public static class InteractionEvents
{
    public static System.Action<IInteractable> OnCurrentChanged { get; set; }
    public static System.Action<IInteractable, PlayerInteractor> OnInteracteRequested { get; set; }
    public static System.Action<List<UpgradeOption>, PlayerInteractor> OnOpenUpgradePanel { get; set; }
    public static System.Action OnCloseUpgradePanel { get; set; }

    public static void RaiseCurrentChanged(IInteractable interactable) => OnCurrentChanged?.Invoke(interactable);
    public static void RaiseInteractRequested(IInteractable interactable, PlayerInteractor interactor) => OnInteracteRequested?.Invoke(interactable, interactor);
    public static void RaiseOpenUpgradePanel(List<UpgradeOption> options, PlayerInteractor interactor) => OnOpenUpgradePanel?.Invoke(options, interactor);
    public static void RaiseCloseUpgradePanel() => OnCloseUpgradePanel?.Invoke();
}