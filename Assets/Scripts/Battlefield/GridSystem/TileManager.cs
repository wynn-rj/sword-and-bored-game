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

        // Start is called before the first frame update
        void Start()
        {
            gridHold.maxGridSize = width;
            //GenerateTileMap();
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

                    //This line is supposed to add the tiles to an array, for some reason it doesnt currently work.
                    gridHold.tiles[i, j] = newTile;
                }
            }
        
            tileContainer.position = pos;
        }

        public void EraseTileMap()
        {
            foreach (Transform child in tileContainer)
            {
                Destroy(child.gameObject);
            }

        }

        [ContextMenu("Do Something")]
        void DoSomething()
        {
            Debug.Log("Perform operation");
        }



    }

}
