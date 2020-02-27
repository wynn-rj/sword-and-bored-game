using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Battlefield.CreaturScripts;
using SwordAndBored.Battlefield;
using Cinemachine;
using SwordAndBored.Battlefield.CameraUtilities;
using SwordAndBored.Battlefield.TurnMechanism;
using SwordAndBored.GameData.Database;
using SwordAndBored.GameData.Units;


namespace SwordAndBored.Battlefield
{
    public class EnemyManager : MonoBehaviour
    {
        public int stageDifficulty;
        public int numberOfEnemies;

        public GameObject playerPrefab;
        public CameraManager camManager;
        public TurnManager turnManager;
        public Transform unitHolder;
        public GameObject indicator;
        public GridHolder grid;

        void Awake()
        {
            for(int numUnits=0; numUnits < 1; numUnits++)
            {
                IEnemy enemyData = Enemy.GetEnemyFromTier(1);

                GameObject unit = Instantiate(playerPrefab, new Vector3(numUnits, 1.5f, 5), Quaternion.identity);
                UniqueCreature uniqueCreature = unit.GetComponent<UniqueCreature>();
                UnitAbilitiesContainer abilities = unit.GetComponent<UnitAbilitiesContainer>();
                UnitStats stats = unit.GetComponent<UnitStats>();
                CinemachineVirtualCamera cam = uniqueCreature.currentCamera;
                BrainManager brain = unit.GetComponent<BrainManager>();
                camManager.cameras.Add(cam.gameObject);
                turnManager.units.Add(unit);
                //turnManager.activePlayer = brain;
                Animator anim = unit.GetComponent<Animator>();
                RuntimeAnimatorController currentAi = Resources.Load<RuntimeAnimatorController>("Ai/StrikerBrain");
                anim.runtimeAnimatorController = currentAi;
                brain.manager = turnManager;

                //UniqueCreature
                uniqueCreature.creatureName = enemyData.Name;
                uniqueCreature.maxHealth = enemyData.Stats.HP;
                uniqueCreature.maxMovement = enemyData.Stats.Movement;
                uniqueCreature.isEnemy = true;
                uniqueCreature.gridHolder = grid;


                //stats
                stats.health = enemyData.Stats.HP;
                stats.attack = enemyData.Stats.Physical_Attack;
                stats.magicAttack = enemyData.Stats.Magic_Attack;
                stats.defense = enemyData.Stats.Physical_Defense;
                stats.magicDefense = enemyData.Stats.Magic_Defense;
                stats.movement = enemyData.Stats.Movement;
                stats.speedIntit = enemyData.Stats.Initiative;
                stats.accuracy = enemyData.Stats.Accuracy;
                stats.evasion = enemyData.Stats.Evasion;

                //brain
                brain.tileIndictor = indicator;
                brain.startCoordinates = new Vector2(25 - numUnits * 2, 27);

                unit.transform.parent = unitHolder;

                turnManager.enemies.Add(unit);
            }
        }
        
        void Update()
        {
            
        }
    }
}
