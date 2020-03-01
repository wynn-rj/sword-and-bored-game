using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Terrain;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using SwordAndBored.Strategy.GameResources;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.ProceduralTerrain
{
    public class TileManager : MonoBehaviour
    { 
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
        public Gold gold;
        public GameObject goldCity;

        private IDictionary<System.Type, GameObject> terrainToGameObject;
        private System.Random fixedRandom;

        private void Start()
        {
            terrainToGameObject = new Dictionary<System.Type, GameObject>()
            {
                { typeof(PlayerTerritoryTerrainComponent), playerTile },
                { typeof(EnemyTerritoryTerrainComponent), enemyTile },
                { typeof(GrasslandTerrainComponent), plainTile },
                { typeof(ForestTerrainComponent), forestTile },
                { typeof(DesertTerrainComponent), desertTile },
                { typeof(MountainTerrainComponent), mountainTile },
                { typeof(SnowTerrainComponent), snowMountainTile },
                { typeof(RiverTerrainComponent), riverTile },
            };
            foreach (KeyValuePair<System.Type, GameObject> kv in terrainToGameObject)
            {
                kv.Value.transform.localScale = new Vector3(Constants.hexRadius, Constants.hexRadius, 1);
                kv.Value.transform.rotation = Quaternion.Euler(-90, 0, 90);
            }

            fixedRandom = new System.Random(12345);
            hexTiling = new HexGrid(Constants.hexRadius, Constants.mapWidth, Constants.mapHeight);
            PrepareTiles();
            BuildTiles();
        }

        private void PrepareTiles()
        {
            int xDim = Constants.mapWidth / 2;
            int yDim = Constants.mapHeight / 2;
            int enemyBaseX = xDim - Constants.xMargin;
            int enemyBaseY = yDim - Constants.yMargin;
            float creepSpreadModifier = Mathf.Pow((Constants.mapWidth / 5f), 2f);

            foreach (IHexGridCell tile in hexTiling.AllCells)
            {
                Point<int> gridPoint = tile.Position.GridPoint;
                bool activeCreep = (Mathf.Pow(gridPoint.X - enemyBaseX, 2f) / creepSpreadModifier) + (Mathf.Pow(gridPoint.Y - enemyBaseY, 2f) / creepSpreadModifier) - Random.Range(0, 0.2f) < 1;
                ITerrainComponent terrainComponent = GetTileTerrain(tile);
                tile.AddComponent(terrainComponent);
                if (!(terrainComponent is RiverTerrainComponent))
                {
                    tile.AddComponent(new CreepComponent(activeCreep));
                }
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

            hexTiling[-xDim + Constants.xMargin + 5, -yDim + Constants.yMargin + 5].AddComponent(new GoldCityComponent(gold));
        }

        private void BuildTiles()
        {
            Vector3 tileHeight = new Vector3(0, -1, 0);
            GameObject hexMap = new GameObject("HexTiling");
            System.Type[] tileHolderComponents = new System.Type[] { typeof(MonoHexGridCell) };

            foreach (IHexGridCell tile in hexTiling.AllCells)
            {
                Vector3 tileLocation = new Vector3(tile.Position.Center.X, 0, tile.Position.Center.Y);
                GameObject tileHolder = new GameObject(TileName(tile), tileHolderComponents);
                tileHolder.transform.position = tileLocation;
                tileHolder.transform.parent = hexMap.transform;
                tileHolder.GetComponent<MonoHexGridCell>().HexGridCell = tile;
                tile.AddComponent(new GameObjectComponent(tileHolder));

                GameObject terrainPrefab = terrainToGameObject[tile.GetComponent<ITerrainComponent>().GetType()];
                AddToTileHolder(tileHolder, terrainPrefab, tileHeight, Constants.terrainObjectName);
                if (tile.HasComponent<CreepComponent>())
                {
                    AddToTileHolder(tileHolder, enemyCreepTile, tileHeight, Constants.creepObjectName);
                    tile.GetComponent<CreepComponent>().SetTileHolder(tileHolder);
                }

                if (tile.HasComponent<GoldCityComponent>())
                {
                    AddToTileHolder(tileHolder, goldCity);
                }
            }
        }

        private void CreateBase(int x, int y, bool isPlayer, IDictionary<IHexGridCell, bool> baseTerrains)
        {
            IHexGridCell centerBaseTile = hexTiling[x, y];
            centerBaseTile.RemoveComponent<CreepComponent>();
            centerBaseTile.AddComponent((isPlayer) ? (new PlayerBaseComponent() as ICellComponent) : new EnemyBaseComponent());
            baseTerrains.Add(centerBaseTile, isPlayer);
            foreach (IHexGridCell neighbor in hexTiling.CellNeighbors(centerBaseTile))
            {
                baseTerrains.Add(neighbor, isPlayer);
                neighbor.RemoveComponent<CreepComponent>();
            }
            hexTiling[x, y].AddComponent(new GoldCityComponent(gold));
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
            else if (height < Constants.riverHeightThreshold && waterLevel > Constants.riverWaterLevelThreshold)
            {
                terrain = new RiverTerrainComponent();
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
            return Mathf.FloorToInt(Mathf.PerlinNoise((gridPoint.X * Constants.hexRadius) / 25f + 1000, (gridPoint.Y * Constants.hexRadius) / 25f + 1000) * 30);
        }

        private float GetTileWaterLevel(IHexGridCell tile)
        {
            Point<int> gridPoint = tile.Position.GridPoint;
            return (Mathf.Pow(((-gridPoint.X + (Constants.mapWidth / 2f)) / Constants.mapWidth) - 0.5f, 3) + 0.5f)
                 * (Mathf.Pow(((-gridPoint.Y + (Constants.mapHeight / 2f)) / Constants.mapHeight) + 0.5f, 1 / 3f) + 0.75f) 
                 + ((float)fixedRandom.NextDouble() * 0.05f);
        }

        private static string TileName(IHexGridCell tile)
        {
            return string.Format("hextile_{0}_{1}", tile.Position.GridPoint.X, tile.Position.GridPoint.Y);
        }

        private static GameObject AddToTileHolder(GameObject tileHolder, GameObject prefab, Vector3 atLocation = default, string name = null)
        {
            GameObject gameObject = Instantiate(prefab, tileHolder.transform, false);
            if (atLocation != default)
            {
                gameObject.transform.localPosition = atLocation;
            }
            if (name != null)
            {
                gameObject.name = name;
            }
            return gameObject;
        }
    }
}
