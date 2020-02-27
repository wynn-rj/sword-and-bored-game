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
        public GridHolder gridHold;
        public Tile[,] grid;
        public int size = 50;

        // Start is called before the first frame update
        void Start()
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
                    tempTile.grid = gridHold;
                    grid[i,j] = tempTile;
                }
            }
        
            tileContainer.position = pos;

            gridHold.tiles = grid;
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
