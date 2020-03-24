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

        float a = .1f;
        float b = 0;

        public SpriteRenderer square;

        public Tile[,] grid;

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

        private void Update()
        {
            if (b > Time.time)
            {
                square.enabled = true;
            } else
            {
                square.enabled = false;
            }
        }

        public void Highlight(Color color)
        {
            Color c = new Color(color.r, color.g, color.b, 20);
            square.color = c;
        }

    }
}
