using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHolder : MonoBehaviour
{
    public int size = 50;

    public Tile[,] tiles;

    private void Start()
    {
        int k = 0;
        tiles = new Tile[size, size];
        Tile[] children = GetComponentsInChildren<Tile>();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size && k < children.Length; j++)
            {
                tiles[i, j] = children[k];
                k++;
            }
        }    
    }
}
