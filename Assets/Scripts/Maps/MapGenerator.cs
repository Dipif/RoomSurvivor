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
    public float CeilingOffsetX = 0f;
    public float CeilingOffsetY = 3f;
    public float CeilingOffsetZ = 0f;

    [Header("Prefabs")]
    public GameObject RoomPrefab;
    public GameObject PathPrefab;
    public GameObject WallPrefab;
    public GameObject CeilingPrefab;


    [Header("References")]
    public RoomManager RoomManager;
    public NavMeshSurface NavMeshSurface;

    bool isGenerating = false;

    // Track previous values to detect changes
    private int prevHCount;
    private int prevVCount;
    private float prevHSpacing;
    private float prevVSpacing;

    public void Start()
    {
        CachePrev();
        RebuildAll();
    }

    public void Update()
    {
#if UNITY_EDITOR
        // Check if room grid dimensions or spacing have changed
        if (HasParamChanged())
        {
            RebuildAll();
            CachePrev();
        }
#endif
    }

    void OnEnable()
    {
#if UNITY_EDITOR
        if (Application.isPlaying) return;
        RebuildAll();
#endif
    }

    void OnDisable()
    {
#if UNITY_EDITOR
        if (Application.isPlaying) return;
        RoomManager.ClearAll();
#endif
    }

    public void RebuildAll()
    {
        if (isGenerating) return;
        isGenerating = true;

        if (!RoomManager) RoomManager = GetComponent<RoomManager>();
        if (!RoomManager) RoomManager = gameObject.AddComponent<RoomManager>();

        RoomManager.ClearAll();
        GenerateMap();
        GenerateNavMesh();

        isGenerating = false;
    }

    public void Clear()
    {
        RoomManager.ClearAll();
        NavMeshSurface.RemoveData();
    }

    void GenerateMap()
    {
        AddRooms();
        AddPaths();
        AddWalls();
        AddCeilings();
    }

    void AddRooms()
    {
        // Generate rooms
        for (int row = 0; row < VerticalCount; row++)
        {
            List<RoomBase> roomRow = new List<RoomBase>();
            for (int col = 0; col < HorizontalCount; col++)
            {
                Vector3 position = GetRoomPosition(row, col);
                RoomManager.CreateRoom(RoomPrefab, position, Quaternion.identity, new Vector2Int(col, row));
            }
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
                RoomManager.CreatePath(PathPrefab, position, Quaternion.identity);
            }
        }

        // Generate vertical paths (connecting rooms in the same column)
        for (int row = 0; row < VerticalCount - 1; row++)
        {
            for (int col = 0; col < HorizontalCount; col++)
            {
                Vector3 position = GetVerticalPathPosition(row, col);
                RoomManager.CreatePath(PathPrefab, position, Quaternion.Euler(0, 90, 0));
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
                RoomManager.CreateWall(WallPrefab, position, Quaternion.identity);
            }
        }

        // Generate vertical walls (connecting rooms in the same column)
        for (int row = 0; row < VerticalCount - 1; row++)
        {
            for (int col = 0; col < HorizontalCount; col++)
            {
                Vector3 position = GetVerticalWallPosition(row, col);
                RoomManager.CreateWall(WallPrefab, position, Quaternion.Euler(0, 90, 0));
            }
        }
    }

    void AddCeilings()
    {
        // Generate rooms
        for (int row = 0; row < VerticalCount; row++)
        {
            for (int col = 0; col < HorizontalCount; col++)
            {
                Vector3 position = GetCeilingPosition(row, col);
                RoomManager.CreateCeiling(CeilingPrefab, position, Quaternion.identity);
            }
        }
    }
    bool HasParamChanged()
    {
        return prevHCount != HorizontalCount
            || prevVCount != VerticalCount
            || !Mathf.Approximately(prevHSpacing, HorizontalSpacing)
            || !Mathf.Approximately(prevVSpacing, VerticalSpacing);
    }

    void CachePrev()
    {
        prevHCount = HorizontalCount;
        prevVCount = VerticalCount;
        prevHSpacing = HorizontalSpacing;
        prevVSpacing = VerticalSpacing;
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
    Vector3 GetCeilingPosition(int row, int col)
    {
        float offsetX = (col - (HorizontalCount - 1) * 0.5f) * HorizontalSpacing + CeilingOffsetX;
        float offsetY = CeilingOffsetY;
        float offsetZ = (row - (VerticalCount - 1) * 0.5f) * VerticalSpacing + CeilingOffsetZ;
        return center.position + new Vector3(offsetX, offsetY, offsetZ);
    }
}