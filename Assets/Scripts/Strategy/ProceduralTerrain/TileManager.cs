using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Terrain;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using SwordAndBored.Strategy.GameResources;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Utilities.Debug;
using SwordAndBored.Utilities.Random;
using SwordAndBored.Strategy.TimeSystem.TimeManager;
using SwordAndBored.UI.Utility;
using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.BaseManagement.Towns;

namespace SwordAndBored.Strategy.ProceduralTerrain
{
    public class TileManager : MonoBehaviour
    { 
        [SerializeField] private List<GameObject> snowMountainTiles;
        [SerializeField] private List<GameObject> mountainTiles;
        [SerializeField] private List<GameObject> forestTiles;
        [SerializeField] private List<GameObject> plainTiles;
        [SerializeField] private List<GameObject> desertTiles;
        [SerializeField] private List<GameObject> riverTiles;
        [SerializeField] private List<GameObject> enemyCreepTiles;
        [SerializeField] private List<GameObject> enemyTiles;
        [SerializeField] private List<GameObject> playerTiles;
        [SerializeField] private ResourceManager resourceManager;
        [SerializeField] private GameObject goldCity;
        [SerializeField] private GameObject playerBase;
        [SerializeField] private GameObject enemyBase;
        [SerializeField] private TimeTrackingTimeManager timeManager;
        [SerializeField] private TownCanvasController townCanvasController;

        private IDictionary<System.Type, List<GameObject>> terrainToGameObject;
        private System.Random fixedRandom;

        public HexGrid HexTiling { get; private set; }

        private void Awake()
        {
            terrainToGameObject = new Dictionary<System.Type, List<GameObject>>()
            {
                { typeof(PlayerTerritoryTerrainComponent), playerTiles },
                { typeof(EnemyTerritoryTerrainComponent), enemyTiles },
                { typeof(GrasslandTerrainComponent), plainTiles },
                { typeof(ForestTerrainComponent), forestTiles },
                { typeof(DesertTerrainComponent), desertTiles },
                { typeof(MountainTerrainComponent), mountainTiles },
                { typeof(SnowTerrainComponent), snowMountainTiles },
                { typeof(RiverTerrainComponent), riverTiles },
            };
            foreach (KeyValuePair<System.Type, List<GameObject>> kv in terrainToGameObject)
            {
                AssertHelper.IsSetInEditor(kv.Value, this);
                AssertHelper.Assert(kv.Value.Count > 0, "Supplied empty list of tiles for " + kv.Key.Name, this);
                foreach (GameObject prefab in kv.Value)
                {
                    prefab.transform.localScale = new Vector3(Constants.hexRadius, Constants.hexRadius, 1);
                    prefab.transform.rotation = Quaternion.Euler(-90, 0, 90);
                }
            }

            GoldCity cityScript = goldCity.GetComponent<GoldCity>();
            cityScript.ResourceManager = this.resourceManager;
            cityScript.UnderPlayerControl = true;
            timeManager.Subscribe(cityScript);

            fixedRandom = new System.Random(12345);
            HexTiling = new HexGrid(Constants.hexRadius, Constants.mapWidth, Constants.mapHeight);
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

            foreach (IHexGridCell tile in HexTiling.AllCells)
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

            foreach (ITown town in Town.GetAllTowns())
            {
                if (town.ID == 0) continue;
                HexTiling[town.X, town.Y].AddComponent(new TownComponent(town, townCanvasController));
            }
        }

        private void BuildTiles()
        {
            Vector3 tileHeight = new Vector3(0, -1, 0);
            GameObject hexMap = new GameObject("HexTiling");
            System.Type[] tileHolderComponents = new System.Type[] { typeof(MonoHexGridCell) };

            foreach (IHexGridCell tile in HexTiling.AllCells)
            {
                Vector3 tileLocation = new Vector3(tile.Position.Center.X, 0, tile.Position.Center.Y);
                GameObject tileHolder = new GameObject(TileName(tile), tileHolderComponents);
                tileHolder.transform.position = tileLocation;
                tileHolder.transform.parent = hexMap.transform;
                tileHolder.GetComponent<MonoHexGridCell>().HexGridCell = tile;
                tile.AddComponent(new GameObjectComponent(tileHolder));

                GameObject terrainPrefab = Odds.SelectAtRandom(terrainToGameObject[tile.GetComponent<ITerrainComponent>().GetType()]);
                GameObject terrain = AddToTileHolder(tileHolder, terrainPrefab, tileHeight, Constants.terrainObjectName);
                
                if (tile.HasComponent<CreepComponent>())
                {
                    GameObject creep = AddToTileHolder(tileHolder, Odds.SelectAtRandom(enemyCreepTiles), tileHeight, Constants.creepObjectName);
                    creep.AddComponent<OnHoverOutline>();
                    tile.GetComponent<CreepComponent>().SetTileHolder(tileHolder);
                }

                if (!tile.HasComponent<UnselectableComponent>())
                {
                    terrain.AddComponent<OnHoverOutline>();
                }

                if (tile.HasComponent<TownComponent>())
                {
                    AddToTileHolder(tileHolder, goldCity);
                }

                if (tile.HasComponent<PlayerBaseComponent>())
                {
                    AddToTileHolder(tileHolder, playerBase);
                }

                if (tile.HasComponent<EnemyBaseComponent>())
                {
                    AddToTileHolder(tileHolder, enemyBase);
                }
            }
        }

        private void CreateBase(int x, int y, bool isPlayer, IDictionary<IHexGridCell, bool> baseTerrains)
        {
            IHexGridCell centerBaseTile = HexTiling[x, y];
            centerBaseTile.RemoveComponent<CreepComponent>();
            centerBaseTile.AddComponent((isPlayer) ? (new PlayerBaseComponent() as ICellComponent) : new EnemyBaseComponent());
            baseTerrains.Add(centerBaseTile, isPlayer);
            foreach (IHexGridCell neighbor in HexTiling.CellNeighbors(centerBaseTile))
            {
                baseTerrains.Add(neighbor, isPlayer);
                neighbor.RemoveComponent<CreepComponent>();
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
