using UnityEngine;

public class RoomBase : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            RoomEvents.RaiseRoomEntered(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            RoomEvents.RaiseRoomExited(this);
        }
    }
}
