using System.Collections.Generic;
using UnityEngine;

public class StatusBase : MonoBehaviour
{
    [SerializeField]
    List<string> Tags = new List<string>();

    protected GameObject owner { get; private set; }
    public virtual void Initialize(GameObject owner)
    {
        this.owner = owner;
    }

    public virtual void ResetStatus() { }

    public void AddTag(string tag)
    {
        if (!Tags.Contains(tag))
        {
            Tags.Add(tag);
        }
    }

    public void RemoveTag(string tag)
    {
        if (Tags.Contains(tag))
        {
            Tags.Remove(tag);
        }
    }

    public bool HasTag(string tag)
    {
        return Tags.Contains(tag);
    }
}
