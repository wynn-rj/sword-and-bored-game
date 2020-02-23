using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public GameObject UnitOnTile { get; set; }
    public bool Walkable { get; set; }
    [HideInInspector]
    public Vector2 CoordinatesOnGrid { get; set; }
    public Vector3 CenterPosition { get { return transform.position; } }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other + "collided into tile " + transform.position + "  :  " + CoordinatesOnGrid);
    }
}