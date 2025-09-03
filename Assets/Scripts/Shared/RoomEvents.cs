using System.Collections.Generic;
using UnityEngine;

public static class RoomEvents
{
    public static System.Action<RoomBase> OnRoomEntered { get; set; }
    public static System.Action<RoomBase> OnRoomExited { get; set; }

    public static void RaiseRoomEntered(RoomBase room) => OnRoomEntered?.Invoke(room);
    public static void RaiseRoomExited(RoomBase room) => OnRoomExited?.Invoke(room);
}