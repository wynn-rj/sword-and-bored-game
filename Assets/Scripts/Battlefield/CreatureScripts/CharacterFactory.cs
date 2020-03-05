﻿using UnityEngine;
using SwordAndBored.Battlefield.CreaturScripts;
using Cinemachine;
using SwordAndBored.Battlefield.CameraUtilities;
using SwordAndBored.Battlefield.TurnMechanism;
using SwordAndBored.GameData.Database;
using SwordAndBored.GameData.Units;
using SwordAndBored.Battlefield.MovementSystemScripts;
using SwordAndBored.GameData.Abilities;
using SwordAndBored.Strategy.Transitions;
using System.Collections.Generic;

namespace SwordAndBored.Battlefield
{
    public class CharacterFactory : MonoBehaviour
    {
        [HideInInspector]
        public Tile[,] grid;
        public TileManager tileManager;
        public GameObject playerPrefab;
        public GameObject indicator;
        public CameraManager camManager;
        public TurnManager turnManager;
        public Transform unitHolder;
    
        void Awake()
        {             
            IEnumerable<IUnit> unitIDs = (SceneSharing.squadID != -1) ? new Squad(SceneSharing.squadID).Units : GetAllUnits();
            grid = tileManager.grid;
            int numUnits = 0;
            foreach (IUnit dataUnit in unitIDs)
            {
                GameObject unit = Instantiate(playerPrefab, new Vector3(numUnits, 1.5f, 0), Quaternion.identity);
                UniqueCreature uniqueCreature = unit.GetComponent<UniqueCreature>();
                UnitAbilitiesContainer abilities = unit.GetComponent<UnitAbilitiesContainer>();
                UnitStats stats = unit.GetComponent<UnitStats>();
                BrainManager brain = unit.GetComponent<BrainManager>();
                MovementSystem ms = unit.GetComponent<MovementSystem>();
                CinemachineVirtualCamera cam = uniqueCreature.currentCamera;
                Animator anim = unit.GetComponent<Animator>();
                RuntimeAnimatorController currentAi = Resources.Load<RuntimeAnimatorController>("Ai/PlayerBrain");
                anim.runtimeAnimatorController = currentAi;
                camManager.cameras.Add(cam.gameObject);
                turnManager.units.Add(unit);
                turnManager.playerUnits.Add(unit);
                turnManager.activePlayer = brain;
                uniqueCreature.isEnemy = false;

                //UniqueCreature
                uniqueCreature.creatureName = dataUnit.Name;
                uniqueCreature.maxHealth = dataUnit.Stats.Max_HP;
                uniqueCreature.health = dataUnit.Stats.Current_HP;
                uniqueCreature.maxMovement = dataUnit.Stats.Movement;

                //Movement
                ms.grid = grid;

                //stats
                stats.health = dataUnit.Stats.Current_HP;
                stats.maxHealth = dataUnit.Stats.Max_HP;
                stats.physicalAttack = dataUnit.Stats.Physical_Attack;
                stats.magicAttack = dataUnit.Stats.Magic_Attack;
                stats.physicalDefense = dataUnit.Stats.Physical_Defense;
                stats.magicDefense = dataUnit.Stats.Magic_Defense;
                stats.movement = dataUnit.Stats.Movement;
                stats.speedIntit = dataUnit.Stats.Initiative;
                stats.role = dataUnit.Role.Name;

                //Abilities
                foreach (IAbility dataAbility in dataUnit.Abilities)
                {
                    abilities.abilities.Add(new Ability(dataAbility));
                }
                


                //brain
                brain.tileIndictor = indicator;
                indicator.GetComponentInChildren<Renderer>().enabled = false;
                brain.startCoordinates = new Vector2(numUnits * 2 + 25, 25);
                brain.manager = turnManager;
                unit.transform.parent = unitHolder;

                numUnits += 2;
            }
        }

        private IEnumerable<IUnit> GetAllUnits() 
        {
            Debug.LogWarning("No squad ID set, defaulting to all units");
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryAllFromTable("Units");
            while (reader.NextRow())
            {
                yield return new Unit(reader.GetIntFromCol("ID"));
            }
            yield return null;
        }
    }
}
