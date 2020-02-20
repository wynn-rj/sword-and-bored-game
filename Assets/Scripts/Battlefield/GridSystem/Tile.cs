using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    
    public GameObject unitOnTile;
    public bool walkable = true;
    [HideInInspector]
    public Vector2 coordinates;

    public Vector3 GetCenterOfTile()
    {
        return transform.position;
    }

    public Vector2 GetCoordinatesOfTileOnGrid()
    {
        return coordinates;
    }
}
