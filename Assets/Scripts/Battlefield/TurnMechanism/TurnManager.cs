using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Battlefield.TurnMechanism;
using UnityEngine.UI;
using TMPro;
using SwordAndBored.Battlefield.CreaturScripts;
using UnityEngine.SceneManagement;
using SwordAndBored.SceneManagement;

namespace SwordAndBored.Battlefield.TurnMechanism
{
    public class TurnManager : MonoBehaviour
    {
        public AudioSource AudioSource;
        [Header("Units")]
        [HideInInspector]
        public List<GameObject> units = new List<GameObject>();
        [HideInInspector]
        public List<GameObject> playerUnits = new List<GameObject>();
        private List<UniqueCreature> startingUnits = new List<UniqueCreature>();
        [HideInInspector]
        public BrainManager activePlayer;
        [Header("UI")]
        TurnOrderController manager;
        public TextMeshProUGUI text;

        public Image actionsLeft;
        [HideInInspector]
        public List<GameObject> enemies = new List<GameObject>();

        public Canvas endGameCanvas;
        public GameObject losePanel;
        public GameObject winPanel;

        public Canvas hotbar;

        public Button endTurnButton;

        [HideInInspector]
        public GameObject statsPanel;
        private bool hasLoadedEndScene = false;

        void Start()
        {
            manager = new TurnOrderController(units.ToArray());//, new RandomShuffler<GameObject>());
            activePlayer = manager.NextEntity().GetComponent<BrainManager>();
            text.text = "Current Player: " + activePlayer.GetName();
            activePlayer.isMyTurn = true;

            // Reset Turn Behaviors
            activePlayer.GetComponent<UniqueCreature>().movementLeft = activePlayer.GetComponent<UniqueCreature>().stats.movement;
            activePlayer.GetComponent<UniqueCreature>().action = true;

            foreach (GameObject obj in playerUnits)
            {
                startingUnits.Add(obj.GetComponent<UniqueCreature>());
            }
        }

        public void nextTurn()
        {
            // End Turn Behavior
            activePlayer.isMyTurn = false;
            UniqueCreature endTurnUnique = activePlayer.GetComponent<UniqueCreature>();
            if (endTurnUnique.stats.IsBleeding)
            {
                endTurnUnique.Damage(endTurnUnique.stats.maxHealth / 8);
            } else if (endTurnUnique.stats.IsBurning)
            {
                endTurnUnique.Damage(endTurnUnique.stats.maxHealth / 8);
            } else if (endTurnUnique.stats.IsStunned)
            {
                endTurnUnique.stats.IsStunned = false;
            }

            //Switches Units
            activePlayer = manager.NextEntity().GetComponent<BrainManager>();
            text.text = "Current Unit: " + activePlayer.GetName();
            activePlayer.isMyTurn = true;

            if(statsPanel)
            {
                statsPanel.SetActive(false);
            }


            UniqueCreature currentUnique = activePlayer.GetComponent<UniqueCreature>();
            // Reset Turn Behaviors
            currentUnique.movementLeft = currentUnique.stats.movement;
            currentUnique.action = true;
        }

        void Update()
        {
            if (activePlayer.GetComponent<UniqueCreature>().isEnemy)
            {
                endTurnButton.interactable = false;
            } else
            {
                endTurnButton.interactable = true;
            }
            if (activePlayer && !activePlayer.GetTurnEnd())
            {
                nextTurn();
            }

            if (activePlayer.HasActionLeft())
            {
                actionsLeft.color = new Color(0, 0, 1, 1f);
            }
            else
            {
                actionsLeft.color = new Color(1, 0, 0, 1f);
            }

            if (activePlayer.creature.isEnemy)
            {
                hotbar.enabled = false;
                
            } else
            {
                hotbar.enabled = true;
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                }
            }

            if (enemies.Count == 0 || playerUnits.Count == 0 && !hasLoadedEndScene)
            {
                EndBattle(enemies.Count == 0);
            }
            CheckStatusForAll();
        }

        public void EndBattle(bool playerWin)
        {
            hasLoadedEndScene = true;
            if (playerWin)
            {
                winPanel.SetActive(true);
                // Enable Win Panel
            } else
            {
                losePanel.SetActive(true);
                // Enable Lose Panel
            }
            endGameCanvas.GetComponentInChildren<Animator>().SetTrigger("Win");
            hotbar.enabled = false;
            SaveAllUnits();
            GameScenes.battleWin = playerWin;
        }

        public void RemoveUnitFromList(GameObject unit)
        {
            units.Remove(unit);
            manager.RemoveEntity(unit);
        }

        public void RemoveUnitFromEnemyList(GameObject unit)
        {
            enemies.Remove(unit);
        }

        public void RemoveUnitFromPlayerList(GameObject unit)
        {
            playerUnits.Remove(unit);
        }

        public void SaveAllUnits()
        {
            foreach (UniqueCreature creature in startingUnits)
            {
                creature.myUnit.Save();
            }
        }

        public void CheckStatusForAll()
        {
            foreach (GameObject unit in units)
            {
                //Check Status
                UniqueCreature unitCreature = unit.GetComponent<UniqueCreature>();
                if (unitCreature.stats.IsBleeding)
                {
                    unitCreature.stats.magicDefense = unitCreature.stats.magicDefenseMax / 2;
                }
                else if (unitCreature.stats.IsBurning)
                {
                    unitCreature.stats.physicalAttack = unitCreature.stats.physicalAttackMax / 2;
                }
                else if (unitCreature.stats.IsStunned)
                {
                    unitCreature.stats.movement = 0;
                }
                else if (unitCreature.stats.IsFrozen)
                {
                    unitCreature.stats.physicalDefense = unitCreature.stats.physicalDefenseMax / 2;
                    unitCreature.stats.movement = unitCreature.stats.movementMax / 2;
                }
            }
        }
    }
}
