using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Terrain;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.ProceduralTerrain
{
    public class TileManager : MonoBehaviour
    {        
        private int xDim;
        private int yDim;
        
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
        public HexGrid hexTiling;

        private void Start()
        {
            hexMap = new GameObject();
            hexMap.name = "HexTiling";

            xDim = Constants.mapWidth / 2;
            yDim = Constants.mapHeight / 2;

            hexTiling = new HexGrid(Constants.hexRadius, Constants.mapWidth, Constants.mapHeight);
            PrepareTiles();
            BuildTiles();
        }

        private void PrepareTiles()
        {
            int enemyBaseX = xDim - Constants.xMargin;
            int enemyBaseY = yDim - Constants.yMargin;
            float creepSpreadModifier = Mathf.Pow((Constants.mapWidth / 5f), 2f);

            foreach (IHexGridCell tile in hexTiling.AllCells)
            {
                Point<int> gridPoint = tile.Position.GridPoint;
                if ((Mathf.Pow(gridPoint.X - enemyBaseX, 2f) / creepSpreadModifier) + (Mathf.Pow(gridPoint.Y - enemyBaseY, 2f) / creepSpreadModifier) - Random.Range(0, 0.2f) < 1)
                {
                    ITerrainComponent creep = new CreepTerrainComponent()
                    {
                        Height = GetTileHeight(tile),
                        WaterLevel = GetTileWaterLevel(tile)
                    };
                    tile.AddComponent(creep);
                    continue;
                }
                tile.AddComponent(GetTileTerrain(tile));
            }

            IDictionary<IHexGridCell, bool> baseTerrains = new Dictionary<IHexGridCell, bool>();
            CreateBase(-xDim + Constants.xMargin, -yDim + Constants.yMargin, true, baseTerrains);
            CreateBase(enemyBaseX, enemyBaseY, false, baseTerrains);

            foreach (KeyValuePair<IHexGridCell, bool> tileTerrains in baseTerrains)
            {
                tileTerrains.Key.RemoveComponent<ITerrainComponent>();
                ITerrainComponent newTerrain = (tileTerrains.Value) ? (new PlayerTerritoryTerrainComponent() as ITerrainComponent)
                    : new EnemyTerritoryTerrainComponent();
                newTerrain.Height = GetTileHeight(tileTerrains.Key);
                newTerrain.WaterLevel = GetTileWaterLevel(tileTerrains.Key);
                tileTerrains.Key.AddComponent(newTerrain);
            }            
        }

        private void BuildTiles()
        {
            Quaternion tileRotation = Quaternion.Euler(-90, 0, 90);
            IDictionary<System.Type, GameObject> terrainToGameObject = new Dictionary<System.Type, GameObject>()
            {
                { typeof(CreepTerrainComponent), enemyCreepTile },
                { typeof(PlayerTerritoryTerrainComponent), playerTile },
                { typeof(EnemyTerritoryTerrainComponent), enemyTile },
                { typeof(GrasslandTerrainComponent), plainTile },
                { typeof(ForestTerrainComponent), forestTile },
                { typeof(DesertTerrainComponent), desertTile },
                { typeof(MountainTerrainComponent), mountainTile },
                { typeof(SnowTerrainComponent), snowMountainTile },
                { typeof(RiverTerrainComponent), riverTile },
            };
            foreach (GameObject gameObject in terrainToGameObject.Values)
            {
                gameObject.transform.localScale = new Vector3(Constants.hexRadius, Constants.hexRadius, Constants.hexRadius * 6);
            }
            foreach (IHexGridCell tile in hexTiling.AllCells)
            {
                ITerrainComponent terrain = tile.GetComponent<ITerrainComponent>();
                Vector3 tileLocation = new Vector3(tile.Position.Center.X, terrain.Height, tile.Position.Center.Y);
                GameObject tilePrefab = Instantiate(terrainToGameObject[terrain.GetType()], tileLocation, tileRotation);
                tilePrefab.transform.parent = hexMap.transform;
            }
        }

        private void CreateBase(int x, int y, bool isPlayer, IDictionary<IHexGridCell, bool> baseTerrains)
        {
            IHexGridCell centerBaseTile = hexTiling[x, y];
            centerBaseTile.AddComponent((isPlayer) ? (new PlayerBaseComponent() as ICellComponent) : new EnemyBaseComponent());
            baseTerrains.Add(centerBaseTile, isPlayer);
            foreach (IHexGridCell neighbor in hexTiling.CellNeighbors(centerBaseTile))
            {
                baseTerrains.Add(neighbor, isPlayer);
            }
        }

        private ITerrainComponent GetTileTerrain(IHexGridCell tile)
        {
            int height = GetTileHeight(tile);
            float waterLevel = GetTileWaterLevel(tile);
            ITerrainComponent terrain;

            if (height > Constants.mountainHeightThreshold)
            {
                bool isSnowy = waterLevel > Constants.snowMountainWaterLvlThreshold && height > Constants.snowMountainHeightThreshold;
                terrain = (isSnowy) ? (new SnowTerrainComponent() as ITerrainComponent) : new MountainTerrainComponent();
            }
            else
            {
                if (waterLevel > Constants.forestWaterLevelThreshold)
                {
                    terrain = new ForestTerrainComponent();
                }
                else if (waterLevel > Constants.plainsWaterLevelThreshold)
                {
                    terrain = new GrasslandTerrainComponent();
                }
                else
                {
                    terrain = new DesertTerrainComponent();
                }
            }

            terrain.Height = height;
            terrain.WaterLevel = waterLevel;
            return terrain;
        }

        private int GetTileHeight(IHexGridCell tile)
        {
            Point<int> gridPoint = tile.Position.GridPoint;
            return Mathf.FloorToInt(Mathf.PerlinNoise(gridPoint.X / 25f + 1000, gridPoint.Y / 25f + 1000) * 30);
        }

        private float GetTileWaterLevel(IHexGridCell tile)
        {
            Point<int> gridPoint = tile.Position.GridPoint;
            return (Mathf.Pow(((gridPoint.X + (Constants.mapWidth / 2f)) / Constants.mapWidth) - 0.5f, 3) + 0.5f)
                 * (Mathf.Pow(((gridPoint.Y + (Constants.mapHeight / 2f)) / Constants.mapHeight) + 0.5f, 1 / 3f) + 0.75f) + Random.Range(0, 0.035f);
        }
    }
}
