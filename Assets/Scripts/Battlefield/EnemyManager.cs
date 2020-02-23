﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Battlefield.CreaturScripts;
using SwordAndBored.Battlefield;
using Cinemachine;
using SwordAndBored.Battlefield.CameraUtilities;
using SwordAndBored.Battlefield.TurnMechanism;
using SwordAndBored.GameData.Database;
using SwordAndBored.GameData.Database.Tables;


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

        void Awake()
        {
            //Database connection goes here
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryAllFromTable("Units");
            int numUnits = 0;
            while (reader.NextRow())
            {
                UnitTable unitTable = new UnitTable(reader.GetIntFromCol("ID"));

                GameObject unit = Instantiate(playerPrefab, new Vector3(numUnits, 1.5f, 5), Quaternion.identity);
                UniqueCreature uniqueCreature = unit.GetComponent<UniqueCreature>();
                UnitAbilitiesContainer abilities = unit.GetComponent<UnitAbilitiesContainer>();
                UnitStats stats = unit.GetComponent<UnitStats>();
                CinemachineVirtualCamera cam = uniqueCreature.currentCamera;
                BrainManager brain = unit.GetComponent<BrainManager>();
                camManager.cameras.Add(cam.gameObject);
                turnManager.units.Add(unit);
                turnManager.activePlayer = brain;
                Animator anim = unit.GetComponent<Animator>();
                RuntimeAnimatorController currentAi = Resources.Load<RuntimeAnimatorController>("Ai/StrikerBrain");
                anim.runtimeAnimatorController = currentAi;

                //UniqueCreature
                uniqueCreature.creatureName = unitTable.Descriptor.Name;
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
                stats.role = unitTable.Role.Descriptor.Name;

                //brain
                brain.tileIndictor = indicator;
                brain.startCoordinates = new Vector2(numUnits, 10);

                unit.transform.parent = unitHolder;

                turnManager.enemies.Add(unit);
                numUnits += 2;
            }
        }
        
        void Update()
        {
            
        }
    }
}
