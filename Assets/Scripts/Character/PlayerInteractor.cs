using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private readonly List<IInteractable> candidates = new List<IInteractable>();
    public IInteractable Current { get; private set; }

    public void AddCandidate(IInteractable interactable)
    {
        if (!candidates.Contains(interactable))
        {
            candidates.Add(interactable);
            UpdateCurrent();
        }
    }

    public void RemoveCandidate(IInteractable interactable)
    {
        if (candidates.Contains(interactable))
        {
            candidates.Remove(interactable);
            UpdateCurrent();
        }
    }

    private void UpdateCurrent()
    {
        IInteractable newCurrent = null;
        float closestDistance = float.MaxValue;
        foreach (var candidate in candidates)
        {
            var candidateTransform = (candidate as MonoBehaviour).transform;
            float distance = Vector3.Distance(transform.position, candidateTransform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                newCurrent = candidate;
            }
        }
        if (newCurrent != Current)
        {
            Current = newCurrent;
            InteractionEvents.RaiseCurrentChanged(Current);
        }
    }
}
