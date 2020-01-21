using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [HideInInspector]
    public GameObject unitOnTile;
    public bool walkable = true;

    public Vector3 GetPos()
    {
        return transform.position;
    }
}
