using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBiome
{
    List<IHexGridCell> tiles;

    private GameObject biomeObject;

    public GameObject BiomeObject
    {
        get
        {
            return biomeObject;        
        }
        set
        {
            biomeObject = value;
        }
    }

    protected float heightModifier;

    public AbstractBiome(string name)
    {
        this.tiles = new List<IHexGridCell>();
        this.biomeObject = new GameObject();
        this.biomeObject.name = name;
    }

    public void AddTile(IHexGridCell tile, GameObject tileObject)
    {
        tileObject.transform.parent = biomeObject.transform;
        tiles.Add(tile);
    }
}
