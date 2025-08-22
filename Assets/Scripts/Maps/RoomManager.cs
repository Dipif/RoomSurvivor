using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    List<List<RoomBase>> rooms = new List<List<RoomBase>>();
    List<RS_Path> paths = new List<RS_Path>();
    List<Wall> walls = new List<Wall>();
    List<Ceiling> ceilings = new List<Ceiling>();
}
