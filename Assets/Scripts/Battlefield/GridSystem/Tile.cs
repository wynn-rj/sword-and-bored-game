using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SwordAndBored.Battlefield
{
    public class Tile : MonoBehaviour
    {
    
        public GameObject unitOnTile;
        public bool walkable = true;
        [HideInInspector]
        public Vector2 coordinates;
        [HideInInspector]
        public int x;
        [HideInInspector]
        public int y;

        [HideInInspector]
        public int f = 0;
        [HideInInspector]
        public int g = 0;
        [HideInInspector]
        public int h = 0;
        [HideInInspector]
        Vector2 previous;


        public Tile Up;
        public Tile Left;
        public Tile Right;
        public Tile Down;
    

        public GridHolder grid;

        public Vector3 GetCenterOfTile()
        {
            return transform.position;
        }

        public Vector2 GetCoordinatesOfTileOnGrid()
        {
            return coordinates;
        }

        public void setCoords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    
    }
}
