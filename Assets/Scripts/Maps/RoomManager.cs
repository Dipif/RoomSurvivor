using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("Parents (auto-created if null)")]
    public Transform RoomsRoot;
    public Transform PathsRoot;
    public Transform WallsRoot;
    public Transform CeilingsRoot;

    Dictionary<Vector2Int, RoomBase> rooms = new Dictionary<Vector2Int, RoomBase>();
    List<RS_Path> paths = new List<RS_Path>();
    List<Wall> walls = new List<Wall>();
    List<Ceiling> ceilings = new List<Ceiling>();

    public RoomBase CurrentRoom;

    private void Awake()
    {
        EnsureRoots();
    }

    private void OnEnable()
    {
        RoomEvents.OnRoomEntered += (room) =>
        {
            CurrentRoom = room;
        };

        RoomEvents.OnRoomExited += (room) =>
        {
            if (room == CurrentRoom)
                CurrentRoom = null;
        };
    }

    private void OnDisable()
    {
        RoomEvents.OnRoomEntered -= (room) =>
        {
            CurrentRoom = room;
        };
        RoomEvents.OnRoomExited -= (room) =>
        {
            if (room == CurrentRoom)
                CurrentRoom = null;
        };
    }

    void EnsureRoots()
    {
        if (RoomsRoot == null)
        {
            RoomsRoot = new GameObject("Rooms").transform;
            RoomsRoot.SetParent(transform);
        }
        if (PathsRoot == null)
        {
            PathsRoot = new GameObject("Paths").transform;
            PathsRoot.SetParent(transform);
        }
        if (WallsRoot == null)
        {
            WallsRoot = new GameObject("Walls").transform;
            WallsRoot.SetParent(transform);
        }
        if (CeilingsRoot == null)
        {
            CeilingsRoot = new GameObject("Ceilings").transform;
            CeilingsRoot.SetParent(transform);
        }
    }

    public RoomBase CreateRoom(GameObject prefab, Vector3 pos, Quaternion rot, Vector2Int grid)
    {
        var comp = Instantiate(prefab, pos, rot, RoomsRoot);
        rooms[grid] = comp.GetComponent<RoomBase>();
        return rooms[grid];
    }

    public RS_Path CreatePath(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        var comp = Instantiate(prefab, pos, rot, PathsRoot);
        var path = comp.GetComponent<RS_Path>();
        paths.Add(path);
        return path;
    }

    public Wall CreateWall(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        var comp = Instantiate(prefab, pos, rot, WallsRoot);
        var wall = comp.GetComponent<Wall>();
        walls.Add(wall);
        return wall;
    }

    public Ceiling CreateCeiling(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        var comp = Instantiate(prefab, pos, rot, CeilingsRoot);
        var ceiling = comp.GetComponent<Ceiling>();
        ceilings.Add(ceiling);
        return ceiling;
    }

    public void ClearAll()
    {
        foreach (var kv in rooms)
            SafeDistroy(kv.Value.gameObject);
        rooms.Clear();

        foreach (var path in paths)
            SafeDistroy(path.gameObject);
        paths.Clear();

        foreach (var wall in walls)
            SafeDistroy(wall.gameObject);
        walls.Clear();

        foreach (var ceiling in ceilings)
            SafeDistroy(ceiling.gameObject);
        ceilings.Clear();
    }

    void SafeDistroy(GameObject obj)
    {
        if (obj == null) return;
#if UNITY_EDITOR
        if (Application.isPlaying)
            Destroy(obj);
        else 
            DestroyImmediate(obj);
#else
        Destroy(obj);
#endif
    }
}
