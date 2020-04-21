using SwordAndBored.GameData.Units;
using SwordAndBored.SceneManagement;
using SwordAndBored.Strategy.EnemyManagement;
using SwordAndBored.Strategy.Movement;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Terrain;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using SwordAndBored.Strategy.Squads;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SwordAndBored.Strategy.Transitions
{
    class BattleStarter : MonoBehaviour
    {
        [SerializeField] private EnemyBrain enemyBrain;
        [SerializeField] private SquadManager squadManager;

        private static BattleStarter Instance { get; set; }

        private void Awake()
        {
            Instance = this;   
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public static void StartBattle(SquadController squadController)
        {
            StartBattle(squadController.Location, squadController.SquadData);
        }

        public static void StartBattle(IHexGridCell location, ISquad squadData)
        {
            SceneSharing.biome = location.GetComponent<ITerrainComponent>().GetType().Name;
            if (location.HasComponent<TownComponent>())
            {
                SceneSharing.biome = "Town";
            }
            SceneSharing.squadID = squadData.ID;
            foreach (SquadController squad in Instance.squadManager.squads)
            {
                squad.ClearPath();
            }
            foreach (EnemyMovementController squad in Instance.enemyBrain.Enemies)
            {
                squad.ClearPath();
            }
            Debug.Log(string.Format("Starting a battle with squad {0} on biome {1}", SceneSharing.squadID, SceneSharing.biome));
            SceneManager.LoadSceneAsync(GameScenes.BATTLEFIELD);
        }
    }
}
