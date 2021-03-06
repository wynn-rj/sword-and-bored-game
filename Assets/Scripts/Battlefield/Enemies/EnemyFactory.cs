﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Battlefield.CreaturScripts;
using SwordAndBored.Battlefield;
using Cinemachine;
using SwordAndBored.Battlefield.CameraUtilities;
using SwordAndBored.Battlefield.TurnMechanism;
using SwordAndBored.GameData.Database;
using SwordAndBored.GameData.Units;
using SwordAndBored.Battlefield.MovementSystemScripts;
using SwordAndBored.GameData.Abilities;


namespace SwordAndBored.Battlefield
{
    public class EnemyFactory : MonoBehaviour
    {
        public int stageDifficulty;
        public int numberOfEnemies;
        public TileManager tileManager;
        public GameObject playerPrefab;
        public CameraManager camManager;
        public TurnManager turnManager;
        public Transform unitHolder;
        public GameObject indicator;
        [HideInInspector]
        public Tile[,] grid;

        public Vector2 startCoords;

        void Awake()
        {

            grid = tileManager.grid;
            for (int numUnits=0; numUnits < 3; numUnits++)
            {
                IEnemy enemyData = Enemy.GetEnemyFromTier(1);

                GameObject unit = Instantiate(playerPrefab, new Vector3(numUnits, 1.5f, 5), Quaternion.identity);
                UniqueCreature uniqueCreature = unit.GetComponent<UniqueCreature>();
                UnitAbilitiesContainer abilities = unit.GetComponent<UnitAbilitiesContainer>();
                UnitStats stats = unit.GetComponent<UnitStats>();
                CinemachineVirtualCamera cam = uniqueCreature.currentCamera;
                BrainManager brain = unit.GetComponent<BrainManager>();
                MovementSystem ms = unit.GetComponent<MovementSystem>();
                camManager.cameras.Add(cam.gameObject);
                turnManager.units.Add(unit);
                //turnManager.activePlayer = brain;
                Animator anim = unit.GetComponent<Animator>();
                RuntimeAnimatorController currentAi = Resources.Load<RuntimeAnimatorController>("Ai/StrikerBrain");
                anim.runtimeAnimatorController = currentAi;
                brain.manager = turnManager;

                //UniqueCreature
                uniqueCreature.creatureName = enemyData.Name;
                uniqueCreature.health = enemyData.Stats.Current_HP;
                uniqueCreature.maxHealth = enemyData.Stats.Max_HP;
                uniqueCreature.maxMovement = enemyData.Stats.Movement;
                uniqueCreature.isEnemy = true;
                
                //Movement
                ms.grid = grid;

                //Abilities
                foreach (IAbility dataAbility in enemyData.Abilities)
                {
                    abilities.abilities.Add(new Ability(dataAbility));
                }


                //stats
                stats.health = enemyData.Stats.Current_HP;
                stats.maxHealth = enemyData.Stats.Max_HP;
                stats.physicalAttack = enemyData.Stats.Physical_Attack;
                stats.magicAttack = enemyData.Stats.Magic_Attack;
                stats.physicalDefense = enemyData.Stats.Physical_Defense;
                stats.magicDefense = enemyData.Stats.Magic_Defense;
                stats.movement = enemyData.Stats.Movement;
                stats.speedIntit = enemyData.Stats.Initiative;

                //brain
                brain.tileIndictor = indicator;
                //brain.startCoordinates = new Vector2(25 - numUnits * 2, 27);
                brain.startCoordinates = new Vector2(startCoords.x + numUnits, startCoords.y);

                unit.transform.parent = unitHolder;

                turnManager.enemies.Add(unit);
            }
        }
        
        void Update()
        {
            
        }
    }
}
