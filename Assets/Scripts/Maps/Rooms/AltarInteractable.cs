using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AltarInteractable : MonoBehaviour, IInteractable
{

    [SerializeField]
    private List<UpgradeOption> allOptions;

    public void Interact(PlayerInteractor interactor)
    {
        //List<UpgradeOption> options = new List<UpgradeOption>();
        //if (allOptions != null && allOptions.Count > 0)
        //{
        //    var shuffled = new List<UpgradeOption>(allOptions);
        //    int n = shuffled.Count;
        //    for (int i = 0; i < n - 1; i++)
        //    {
        //        int j = Random.Range(i, n);
        //        var temp = shuffled[i];
        //        shuffled[i] = shuffled[j];
        //        shuffled[j] = temp;
        //    }
        //    int count = Mathf.Min(2, shuffled.Count);
        //    options.AddRange(shuffled.GetRange(0, count));
        //}

        InteractionEvents.RaiseOpenUpgradePanel(allOptions, interactor);
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactor = other.GetComponent<PlayerInteractor>();
        if (interactor != null)
        {
            interactor.AddCandidate(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var interactor = other.GetComponent<PlayerInteractor>();
        if (interactor != null)
        {
            interactor.RemoveCandidate(this);
        }
    }
}
