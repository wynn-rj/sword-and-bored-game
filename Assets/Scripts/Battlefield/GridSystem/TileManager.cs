using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;


namespace SwordAndBored.Battlefield.TurnMechanism
{
    public class TileManager : MonoBehaviour
    {
        public GameObject tilePrefab;
        public Transform tileContainer;
        public int width = 25;
        public int height = 25;
        public Tile[,] grid;
        public int size = 50;
        public LayerMask lm;

        private void Awake()
        {
            grid = new Tile[size,size];
            GenerateTileMap();
        }

        public void GenerateTileMap()
        {
            Vector3 pos = transform.position;
            tileContainer.position = new Vector3(width / 2f, 0, height / 2f);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    GameObject newTile = Instantiate(tilePrefab, new Vector3(i, 0, j), Quaternion.identity);
                    newTile.transform.parent = tileContainer;
                    newTile.name = "Tile " + i + " " + j;
                    Tile tempTile = newTile.GetComponent<Tile>();
                    tempTile.coordinates = new Vector2(i, j);
                    tempTile.setCoords(i, j);
                    tempTile.grid = grid;
                    grid[i,j] = tempTile;
                }
            }
        
            tileContainer.position = pos;

            foreach (Tile tile in grid)
            {
                if (Physics.Raycast(tileContainer.TransformPoint(tile.transform.position), Vector3.up, 100, lm))
                {
                    Destroy(tile.gameObject);
                }
            }
        }

        public void EraseTileMap()
        {
            foreach (Transform child in tileContainer)
            {
                Destroy(child.gameObject);
            }

        }

    }

}
