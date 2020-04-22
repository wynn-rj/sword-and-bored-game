using UnityEngine;
using SwordAndBored.Battlefield.CreaturScripts;
using Cinemachine;
using SwordAndBored.Battlefield.CameraUtilities;
using SwordAndBored.Battlefield.TurnMechanism;
using SwordAndBored.GameData.Units;
using SwordAndBored.Battlefield.MovementSystemScripts;
using SwordAndBored.GameData.Abilities;
using SwordAndBored.Strategy.Transitions;
using System.Collections.Generic;
using SwordAndBored.Utilities.Debug;

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
        public Vector2 startCoords;
        public Material active;
        public Material inactive;

        public Material[] warriorMat;
        public Material[] rangerMat;
        public Material[] mageMat;

        public GameObject hat;

        void Awake()
        {
            AssertHelper.Assert(SceneSharing.squadID != -1, "No squad ID set, defaulting to all units", this);
            List<IUnit> unitIDs = (SceneSharing.squadID != -1) ? new Squad(SceneSharing.squadID).Units : Unit.GetAllUnits();
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
                //ParticleSystem particle = Resources.Load<ParticleSystem>("Particles/Fireball");
                //particle = Instantiate(particle);
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
                uniqueCreature.myUnit = dataUnit;

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

                stats.physicalAttackMax = dataUnit.Stats.Physical_Attack;
                stats.magicAttackMax = dataUnit.Stats.Magic_Attack;
                stats.physicalDefenseMax = dataUnit.Stats.Physical_Defense;
                stats.magicDefenseMax = dataUnit.Stats.Magic_Defense;
                stats.movementMax = dataUnit.Stats.Movement;
                stats.speedIntitMax = dataUnit.Stats.Initiative;
                
                stats.role = dataUnit.Role.Name;
                
                stats.IsFrozen = false;
                stats.IsBurning = false;
                stats.IsBleeding = false;
                stats.IsStunned = false;
                stats.FreezeResist = dataUnit.Armor.StatusConditionsResistances.Freeze_Resist;
                stats.BurnResist = dataUnit.Armor.StatusConditionsResistances.Fire_Resist;
                stats.BleedResist = dataUnit.Armor.StatusConditionsResistances.Bleed_Resist;
                stats.StunResist = dataUnit.Armor.StatusConditionsResistances.Stun_Resist;

                //Add Armor for defense
                stats.physicalDefense += dataUnit.Armor.Physical_Defense;
                stats.magicDefense += dataUnit.Armor.Magic_Defense;

                ParticleController particleController = turnManager.particleController;
                //Abilities
                foreach (IAbility dataAbility in dataUnit.Abilities)
                {
                    Ability ab = new Ability(dataAbility);
                    ab.particleController = particleController;
                    ab.active = active;
                    ab.inactive = inactive;
                    abilities.abilities.Add(ab);
                }

                switch (dataUnit.Role.Name)
                {
                    case "Mage":
                        unit.GetComponent<ModelSwitching>().SetColor(mageMat[0], mageMat[1], hat);
                        break;
                    case "Scout":
                        unit.GetComponent<ModelSwitching>().SetColor(rangerMat[0], rangerMat[1], new GameObject());
                        break;
                    case "Warrior":
                        unit.GetComponent<ModelSwitching>().SetColor(warriorMat[0], warriorMat[1], new GameObject());
                        break;
                }

                //brain
                brain.tileIndictor = indicator;
                indicator.GetComponentInChildren<Renderer>().enabled = false;
                //brain.startCoordinates = new Vector2(numUnits * 2 + 25, 25);
                brain.startCoordinates = new Vector2(startCoords.x + numUnits, startCoords.y);
                brain.manager = turnManager;
                unit.transform.parent = unitHolder;

                numUnits += 2;
            }
        }
    }
}
