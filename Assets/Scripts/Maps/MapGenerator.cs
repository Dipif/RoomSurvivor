using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Generate Settings")]
    public Transform center;
    public int HorizontalCount = 3;
    public int VerticalCount = 3;
    public float HorizontalSpacing = 14f;
    public float VerticalSpacing = 14f;

    [Header("Prefabs")]
    public GameObject RoomPrefab;
    public GameObject PathPrefab;
    public GameObject WallPrefab;

    public NavMeshSurface NavMeshSurface;


    Room currentRoom;
    List<List<Room>> rooms = new List<List<Room>>();
    List<Path> paths = new List<Path>();
    List<Wall> walls = new List<Wall>();
    bool isGenerating = false;

    // Track previous values to detect changes
    private int previousHorizontalCount;
    private int previousVerticalCount;
    private float previousHorizontalSpacing;
    private float previousVerticalSpacing;

    public void Start()
    {
        // Initialize previous values
        previousHorizontalCount = HorizontalCount;
        previousVerticalCount = VerticalCount;
        previousHorizontalSpacing = HorizontalSpacing;
        previousVerticalSpacing = VerticalSpacing;

        Clear();
        GenerateMap();
        GenerateNavMesh();
    }

    public void Update()
    {
#if UNITY_EDITOR
        // Check if room grid dimensions or spacing have changed
        if (rooms.Count != VerticalCount ||
            (rooms.Count > 0 && rooms[0].Count != HorizontalCount) ||
            previousHorizontalCount != HorizontalCount ||
            previousVerticalCount != VerticalCount ||
            previousHorizontalSpacing != HorizontalSpacing ||
            previousVerticalSpacing != VerticalSpacing)
        {
            Clear();
            GenerateMap();
            GenerateNavMesh();

            // Update previous values
            previousHorizontalCount = HorizontalCount;
            previousVerticalCount = VerticalCount;
            previousHorizontalSpacing = HorizontalSpacing;
            previousVerticalSpacing = VerticalSpacing;
        }
#endif
    }

    void OnEnable()
    {
        if (Application.isPlaying) return;
        RebuildInEditor();
    }

    void OnDisable()
    {
        if (Application.isPlaying) return;
        Clear();
    }

    public void Clear()
    {
        // Destroy all room GameObjects
        foreach (var roomRow in rooms)
        {
            foreach (var room in roomRow)
            {
                if (room != null && room.gameObject != null)
                {
                    DestroyImmediate(room.gameObject);
                }
            }
        }
        
        // Destroy all path GameObjects
        foreach (var path in paths)
        {
            if (path != null && path.gameObject != null)
            {
                DestroyImmediate(path.gameObject);
            }
        }

        foreach (var wall in walls)
        {
            if (wall != null && wall.gameObject != null)
            {
                DestroyImmediate(wall.gameObject);
            }
        }

        // Clear the lists
        rooms.Clear();
        paths.Clear();
        walls.Clear();

        NavMeshSurface.RemoveData();
    }

    public void RebuildInEditor()
    {
        if (isGenerating) return;
        isGenerating = true;

#if UNITY_EDITOR
        // Prefab 편집 모드에선 미리보기 끄고 싶으면 주석 제거
        // if (PrefabStageUtility.GetCurrentPrefabStage() != null) { _isGenerating = false; return; }
#endif

        Clear();
        GenerateMap();
        GenerateNavMesh();   // 에디터에서도 베이크 가능
        isGenerating = false;
    }

    void GenerateMap()
    {
        AddRooms();
        AddPaths();
        AddWalls();
    }

    void AddRooms()
    {
        // Generate rooms
        for (int row = 0; row < VerticalCount; row++)
        {
            List<Room> roomRow = new List<Room>();
            for (int col = 0; col < HorizontalCount; col++)
            {
                Vector3 position = GetRoomPosition(row, col);
                GameObject roomObject = Instantiate(RoomPrefab, position, Quaternion.identity, transform);
                Room room = roomObject.GetComponent<Room>();
                roomRow.Add(room);
            }
            rooms.Add(roomRow);
        }
    }

    void AddPaths()
    {
        // Generate horizontal paths (connecting rooms in the same row)
        for (int row = 0; row < VerticalCount; row++)
        {
            for (int col = 0; col < HorizontalCount - 1; col++)
            {
                Vector3 position = GetHorizontalPathPosition(row, col);
                GameObject pathObject = Instantiate(PathPrefab, position, Quaternion.identity, transform);
                Path path = pathObject.GetComponent<Path>();
                paths.Add(path);
            }
        }

        // Generate vertical paths (connecting rooms in the same column)
        for (int row = 0; row < VerticalCount - 1; row++)
        {
            for (int col = 0; col < HorizontalCount; col++)
            {
                Vector3 position = GetVerticalPathPosition(row, col);
                GameObject pathObject = Instantiate(PathPrefab, position, Quaternion.Euler(0, 90, 0), transform);
                Path path = pathObject.GetComponent<Path>();
                paths.Add(path);
            }
        }
    }

    void AddWalls()
    {
        // Generate horizontal paths (connecting rooms in the same row)
        for (int row = 0; row < VerticalCount; row++)
        {
            for (int col = 0; col < HorizontalCount - 1; col++)
            {
                Vector3 position = GetHorizontalWallPosition(row, col);
                GameObject wallObject = Instantiate(WallPrefab, position, Quaternion.identity, transform);
                Wall wall = wallObject.GetComponent<Wall>();
                walls.Add(wall);
            }
        }

        // Generate vertical walls (connecting rooms in the same column)
        for (int row = 0; row < VerticalCount - 1; row++)
        {
            for (int col = 0; col < HorizontalCount; col++)
            {
                Vector3 position = GetVerticalWallPosition(row, col);
                GameObject wallObject = Instantiate(WallPrefab, position, Quaternion.Euler(0, 90, 0), transform);
                Wall wall = wallObject.GetComponent<Wall>();
                walls.Add(wall);
            }
        }
    }

    void GenerateNavMesh()
    {
        NavMeshSurface.RemoveData();
        NavMeshSurface.BuildNavMesh();
    }

    
    Vector3 GetRoomPosition(int row, int col)
    {
        float offsetX = (col - (HorizontalCount - 1) * 0.5f) * HorizontalSpacing;
        float offsetZ = (row - (VerticalCount - 1) * 0.5f) * VerticalSpacing;
        return center.position + new Vector3(offsetX, 0, offsetZ);
    }
    Vector3 GetHorizontalPathPosition(int row, int col)
    {
        float offsetX = (col + 0.5f - (HorizontalCount - 1) * 0.5f) * HorizontalSpacing;
        float offsetZ = (row - (VerticalCount - 1) * 0.5f) * VerticalSpacing;
        return center.position + new Vector3(offsetX, 0, offsetZ);
    }
    Vector3 GetVerticalPathPosition(int row, int col)
    {
        float offsetX = (col - (HorizontalCount - 1) * 0.5f) * HorizontalSpacing;
        float offsetZ = (row + 0.5f - (VerticalCount - 1) * 0.5f) * VerticalSpacing;
        return center.position + new Vector3(offsetX, 0, offsetZ);
    }
    Vector3 GetHorizontalWallPosition(int row, int col)
    {
        float offsetX = (col + 0.5f - (HorizontalCount - 1) * 0.5f) * HorizontalSpacing;
        float offsetZ = (row - (VerticalCount - 1) * 0.5f) * VerticalSpacing;
        return center.position + new Vector3(offsetX, -1, offsetZ);
    }
    Vector3 GetVerticalWallPosition(int row, int col)
    {
        float offsetX = (col - (HorizontalCount - 1) * 0.5f) * HorizontalSpacing;
        float offsetZ = (row + 0.5f - (VerticalCount - 1) * 0.5f) * VerticalSpacing;
        return center.position + new Vector3(offsetX, -1, offsetZ);
    }
    
}
