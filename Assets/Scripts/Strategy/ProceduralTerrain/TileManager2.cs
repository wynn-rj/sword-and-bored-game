using SwordAndBored.StrategyView.Map.Grid;
using SwordAndBored.StrategyView.Map.Grid.Cells;
using SwordAndBored.StrategyView.Map.Terrain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager2 : MonoBehaviour
{
    private HexGrid hexTiling;
    private int xDim;
    private int yDim;
    private bool[,] layedTiles;

    private AbstractBiome[] biomes;
    PlainsBiome plains;
    RiverBiome river;

    GameObject hexMap;

    public GameObject snowMountainTile;
    public GameObject mountainTile;
    public GameObject forestTile;
    public GameObject plainTile;
    public GameObject desertTile;
    public GameObject riverTile;
    public GameObject enemyCreepTile;
    public GameObject enemyTile;
    public GameObject playerTile;

    private void Start()
    {
        hexMap = new GameObject();
        hexMap.name = "HexTiling";

        plains = new PlainsBiome();
        plains.BiomeObject.transform.parent = hexMap.transform;

        river = new RiverBiome();
        river.BiomeObject.transform.parent = hexMap.transform;

        xDim = Constants.mapWidth / 2;
        yDim = Constants.mapHeight / 2;

        hexTiling = new HexGrid(Constants.hexRadius, Constants.mapWidth, Constants.mapHeight);

        layedTiles = new bool[Constants.mapWidth + 1, Constants.mapHeight + 1];
        for (int x = 0; x < layedTiles.GetLength(0); x++)
        {
            for (int y = 0; y < layedTiles.GetLength(1); y++)
            {
                layedTiles[x, y] = false;
            }
        }

        for (int i = -xDim; i <= xDim; i++)
        {
            for (int j = -yDim; j <= yDim; j++)
            {
                AbstractTerrainComponent comp = new AbstractTerrainComponent(); 
                hexTiling[i, j].AddComponent(comp);
            }
        }   

        LayAllTiles();
    }

    private void LayAllTiles()
    {
        /*
         * Lay player base
         */

        IHexGridCell centerBaseTile = hexTiling[-xDim + Constants.xMargin, -yDim + Constants.yMargin];
        GameObject one = Instantiate(playerTile, new Vector3(centerBaseTile.Position.Center.X, centerBaseTile.GetComponent<AbstractTerrainComponent>().Height, centerBaseTile.Position.Center.Y), Quaternion.Euler(-90, 0, 90));
        LaySingleTile(-xDim + Constants.xMargin, -yDim + Constants.yMargin);
        one.transform.parent = hexMap.transform;

        foreach (IHexGridCell neighbor in hexTiling.CellNeighbors(-xDim + Constants.xMargin, -yDim + Constants.yMargin))
        {
            GameObject playerPrefab = Instantiate(playerTile, new Vector3(neighbor.Position.Center.X, neighbor.GetComponent<AbstractTerrainComponent>().Height, neighbor.Position.Center.Y), Quaternion.Euler(-90, 0, 90));
            LaySingleTile(neighbor.Position.GridPoint.X, neighbor.Position.GridPoint.Y);
            playerPrefab.transform.parent = hexMap.transform;
        }

        

        /*
         * Lay river tiles first
         */
        List<IHexGridCell> riverTiles = new List<IHexGridCell>();

        IHexGridCell riverMouth = hexTiling[0, 0];
        bool foundTile = false;

        for (int i = -xDim; i < xDim && !foundTile; i++)
        {
            IHexGridCell checkTile = hexTiling[i, yDim - 3];

            if (checkTile.GetComponent<AbstractTerrainComponent>().Height < Constants.riverHeightThreshold && checkTile.GetComponent<AbstractTerrainComponent>().WaterLevel > Constants.riverWaterLevelThreshold)
            {
                riverMouth = checkTile;
                foundTile = true;
            }
        }

        riverTiles.Add(riverMouth);
        int count = 200;
        while (riverTiles.Count > 0)
        {
            IHexGridCell newTile = riverTiles[0];
            riverTiles.RemoveAt(0);

            //Add its neighbors if it meet height and water level reqs
            foreach (IHexGridCell neighbor in hexTiling.CellNeighbors(newTile.Position.GridPoint.X, newTile.Position.GridPoint.Y))
            {
                if (neighbor != null && neighbor.GetComponent<AbstractTerrainComponent>().Height < Constants.riverHeightThreshold && neighbor.GetComponent<AbstractTerrainComponent>().WaterLevel > Constants.riverWaterLevelThreshold && !HasBeenLaid(neighbor.Position.GridPoint.X, neighbor.Position.GridPoint.Y))
                {
                    riverTiles.Add(neighbor);
                    GameObject waterPrefab = Instantiate(riverTile, new Vector3(neighbor.Position.Center.X, Constants.riverHeightThreshold, neighbor.Position.Center.Y), Quaternion.Euler(-90, 0, 90));
                    LaySingleTile(neighbor.Position.GridPoint.X, neighbor.Position.GridPoint.Y);
                    waterPrefab.transform.parent = hexMap.transform;
                }
            }
            count--;
        }

        /*
         * Lay enemy base
         */
        IHexGridCell centerEnemyBaseTile = hexTiling[xDim - Constants.xMargin, yDim - Constants.yMargin];
        GameObject two = Instantiate(enemyTile, new Vector3(centerEnemyBaseTile.Position.Center.X, centerEnemyBaseTile.GetComponent<AbstractTerrainComponent>().Height, centerEnemyBaseTile.Position.Center.Y), Quaternion.Euler(-90, 0, 90));
        LaySingleTile(xDim - Constants.xMargin, yDim - Constants.yMargin);
        two.transform.parent = hexMap.transform;

        foreach (IHexGridCell neighbor in hexTiling.CellNeighbors(xDim - Constants.xMargin, yDim - Constants.yMargin))
        {
            GameObject enemyPrefab = Instantiate(enemyTile, new Vector3(neighbor.Position.Center.X, neighbor.GetComponent<AbstractTerrainComponent>().Height, neighbor.Position.Center.Y), Quaternion.Euler(-90, 0, 90));
            LaySingleTile(neighbor.Position.GridPoint.X, neighbor.Position.GridPoint.Y);
            enemyPrefab.transform.parent = hexMap.transform;
        }

        /*
         * Lay starting enemy creep
         */
        for (int i = -xDim; i <= xDim; i++)
        {
            for (int j = -yDim; j <= yDim; j++)
            {
                if (!HasBeenLaid(i, j))
                {
                    if ((Mathf.Pow(i - centerEnemyBaseTile.Position.GridPoint.X, 2f) / Mathf.Pow((Constants.mapWidth / 5f), 2f)) + (Mathf.Pow(j - centerEnemyBaseTile.Position.GridPoint.Y, 2f) / Mathf.Pow((Constants.mapWidth / 5f), 2)) - Random.Range(0, 0.2f) < 1)
                    {
                        IHexGridCell creepTile = hexTiling[i, j];
                        GameObject creepPrefab = Instantiate(enemyCreepTile, new Vector3(creepTile.Position.Center.X, creepTile.GetComponent<AbstractTerrainComponent>().Height, creepTile.Position.Center.Y), Quaternion.Euler(-90, 0, 90));
                        LaySingleTile(i, j);
                        creepPrefab.transform.parent = hexMap.transform;
                    }
                }
            }
        }

        /*
        for (int i = -xDim + 1, j = yDim - 1; i < xDim && j > -yDim; i++, j--)
        {
            LaySingleTile(i, j);
            IHexGridCell tile = hexTiling[i, j];
            GameObject tilePrefab = Instantiate(riverTile, new Vector3(hexTiling[i, j].Position.Center.X, tile.GetComponent<AbstractTerrainComponent>().Height, hexTiling[i, j].Position.Center.Y), Quaternion.Euler(-90, 0, 90));
            river.AddTile(tile, tilePrefab);

            IEnumerable<IHexGridCell> neighbors = hexTiling.CellNeighbors(i, j);

            //Lay tile for each neighbor too
            foreach (IHexGridCell neighbor in neighbors)
            {
                LaySingleTile(neighbor.Position.GridPoint.X, neighbor.Position.GridPoint.Y);
                GameObject neighborPrefab = Instantiate(riverTile, new Vector3(neighbor.Position.Center.X, tile.GetComponent<AbstractTerrainComponent>().Height, neighbor.Position.Center.Y), Quaternion.Euler(-90, 0, 90));
                river.AddTile(neighbor, neighborPrefab);
            }
        }*/

        /*
        foreach (AbstractBiome biome in biomes)
        { 
            Lay and multiply height modifier
        }*/

        /*
         * Lay terrain
         */
        for (int i = -xDim; i < xDim; i++)
        {
            for (int j = -yDim; j < yDim; j++)
            {
                if (!HasBeenLaid(i, j))
                {
                    IHexGridCell tile = hexTiling[i, j];
                    float tileHeight = tile.GetComponent<AbstractTerrainComponent>().Height;
                    tileHeight -= tileHeight * .5f;
                    float tileWaterLevel = tile.GetComponent<AbstractTerrainComponent>().WaterLevel;
                    GameObject tilePrefab = Instantiate(GetTile(tileHeight, tileWaterLevel), new Vector3(hexTiling[i, j].Position.Center.X, hexTiling[i, j].GetComponent<AbstractTerrainComponent>().Height, hexTiling[i, j].Position.Center.Y), Quaternion.Euler(-90, 0, 90));
                    tilePrefab.transform.parent = hexMap.transform;
                }
            }
        }
    }

    private GameObject GetTile(float height, float waterLevel)
    {
        if (height > Constants.mountainHeightThreshold)
        {
            if (waterLevel > Constants.snowMountainWaterLvlThreshold && height > Constants.snowMountainHeightThreshold)
            {
                return snowMountainTile;
            }

            return mountainTile;
        }
        else
        {
            if (waterLevel > Constants.forestWaterLevelThreshold)
            {
                return forestTile;
            }
            else if (waterLevel > Constants.plainsWaterLevelThreshold)
            {
                return plainTile;
            }
            else
            {
                return desertTile;
            }
        }
    }

    private bool HasBeenLaid(int i, int j)
    {
        return layedTiles[i + xDim, j + yDim];
    }

    private void LaySingleTile(int i, int j)
    {
        layedTiles[i + xDim, j + yDim] = true;
    }
}
