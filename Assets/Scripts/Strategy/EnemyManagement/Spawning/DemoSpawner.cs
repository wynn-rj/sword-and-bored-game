using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.Movement;
using SwordAndBored.Strategy.ProceduralTerrain;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.TimeSystem.TimeManager;
using SwordAndBored.Utilities.Debug;
using UnityEngine;

namespace SwordAndBored.Strategy.EnemyManagement.Spawning
{
    public class DemoSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyMovementController enemyPrefab;
        [SerializeField] private TileManager tileManager;
        [SerializeField] private AbstractTimeManager turnManager;
        [SerializeField] private float enemyPlacementHeight = 0;

#if DEBUG
        void Awake()
        {
            AssertHelper.IsSetInEditor(enemyPrefab, this);
            AssertHelper.IsSetInEditor(tileManager, this);
            AssertHelper.IsSetInEditor(turnManager, this);
        }
#endif

        void Start()
        {
            IEnemy[] enemyList = new IEnemy[] { Enemy.GetEnemyFromTier(1) };
            IHexGridCell enemyBase = tileManager.HexTiling[(Constants.mapWidth / 2) - Constants.xMargin,
                (Constants.mapHeight / 2) - Constants.yMargin];
            foreach (IHexGridCell neighbor in enemyBase.Neighbors)
            {
                PlaceEnemy(enemyList, neighbor);
            }
        }

        private void PlaceEnemy(IEnemy[] enemies, IHexGridCell location)
        {
            EnemyMovementController enemy = Instantiate(enemyPrefab, transform);
            enemy.transform.position = location.Position.CenterAsVector3(enemyPlacementHeight);
            enemy.Enemies = enemies;
            enemy.StartLocation = location;
            turnManager.Subscribe(enemy);
        }
    }
}
