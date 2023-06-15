using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoomSwitch
{
    // Use static class for data persistence
    
    public static Vector2 SpawnPos { get; set; }
    public static float SpawnHealth { get; set; } = 10;
    public static int FacingDirection = 1;



}
