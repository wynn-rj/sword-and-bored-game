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
    public class CharacterFactory : MonoBehaviour
    {
        public GridHolder grid;
        public GameObject playerPrefab;
        public GameObject indicator;
        public CameraManager camManager;
        public TurnManager turnManager;
        public Transform unitHolder;
    
        void Awake()
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryAllFromTable("Units");
            int numUnits = 0;
            while (reader.NextRow())
            {
                Unit unitTable = new Unit(reader.GetIntFromCol("ID"));
        
                GameObject unit = Instantiate(playerPrefab, new Vector3(numUnits, 1.5f, 0), Quaternion.identity);
                UniqueCreature uniqueCreature = unit.GetComponent<UniqueCreature>();
                UnitAbilitiesContainer abilities = unit.GetComponent<UnitAbilitiesContainer>();
                UnitStats stats = unit.GetComponent<UnitStats>();
                BrainManager brain = unit.GetComponent<BrainManager>();
                CinemachineVirtualCamera cam = uniqueCreature.currentCamera;
                camManager.cameras.Add(cam.gameObject);
                turnManager.units.Add(unit);
                turnManager.activePlayer = brain;

                //UniqueCreature
                uniqueCreature.creatureName = unitTable.Name;
                uniqueCreature.maxHealth = unitTable.Stats.HP;
                uniqueCreature.maxMovement = unitTable.Stats.Movement;

                //abilities
                for (int j = 0; j < 5; j++)
                {
                    //abilities.abilities.add();
                }

                //stats
                stats.health = unitTable.Stats.HP;
                stats.attack = unitTable.Stats.Physical_Attack;
                stats.magicAttack = unitTable.Stats.Magic_Attack;
                stats.defense = unitTable.Stats.Physical_Defense;
                stats.magicDefense = unitTable.Stats.Magic_Defense;
                stats.movement = unitTable.Stats.Movement;
                stats.speedIntit = unitTable.Stats.Initiative;
                stats.accuracy = unitTable.Stats.Accuracy;
                stats.evasion = unitTable.Stats.Evasion;
                stats.role = unitTable.Role.Name;

                //brain
                brain.tileIndictor = indicator;
                brain.startCoordinates = new Vector2(numUnits, 5);
                brain.grid = grid;

                unit.transform.parent = unitHolder;

                numUnits += 2;
            }


        }
    
    }

}
