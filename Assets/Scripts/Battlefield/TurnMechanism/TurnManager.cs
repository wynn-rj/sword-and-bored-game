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
        [HideInInspector]
        public BrainManager activePlayer;
        [Header("UI")]
        TurnOrderController manager;
        public TextMeshProUGUI text;

        public Image actionsLeft;
        [HideInInspector]
        public List<GameObject> enemies = new List<GameObject>();

        public Canvas winCanvas;
        public Canvas loseCanvas;

        public Canvas hotbar;

        public Button endTurnButton;

        [HideInInspector]
        public GameObject statsPanel;

        void Start()
        {
            winCanvas.enabled = false;
            loseCanvas.enabled = false;
            manager = new TurnOrderController(units.ToArray());//, new RandomShuffler<GameObject>());
            activePlayer = manager.NextEntity().GetComponent<BrainManager>();
            text.text = "Current Player: " + activePlayer.GetName();
            activePlayer.isMyTurn = true;

            // Reset Turn Behaviors
            activePlayer.GetComponent<UniqueCreature>().movementLeft = activePlayer.GetComponent<UniqueCreature>().stats.movement;
            activePlayer.GetComponent<UniqueCreature>().action = true;
        }

        public void nextTurn()
        {
            // End Turn Behavior
            activePlayer.isMyTurn = false;
            UniqueCreature endTurnUnique = activePlayer.GetComponent<UniqueCreature>();
            if (endTurnUnique.stats.IsBleeding)
            {
                endTurnUnique.Damage(endTurnUnique.stats.maxHealth / 16);
            } else if (endTurnUnique.stats.IsBurning)
            {
                endTurnUnique.Damage(endTurnUnique.stats.maxHealth / 16);
            } else if (endTurnUnique.stats.IsStunned)
            {
                endTurnUnique.stats.IsStunned = false;
            }


            //Switches Units
            activePlayer = manager.NextEntity().GetComponent<BrainManager>();
            text.text = "Current Player: " + activePlayer.GetName();
            activePlayer.isMyTurn = true;

            if(statsPanel)
            {
                statsPanel.SetActive(false);
            }

            //Check Status
            UniqueCreature currentUnique = activePlayer.GetComponent<UniqueCreature>();
            if (currentUnique.stats.IsBleeding)
            {
                currentUnique.stats.magicDefense /= 2;
            }
            else if (currentUnique.stats.IsBurning)
            {
                currentUnique.stats.physicalAttack /= 2;
            } else if (currentUnique.stats.IsStunned)
            {
                currentUnique.stats.movement = 0;
            } else if (currentUnique.stats.IsFrozen)
            {
                currentUnique.stats.physicalDefense /= 2;
                currentUnique.stats.movement /= 2;
            }

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
                actionsLeft.color = new Color(0, 0, 1, .2f);
            }
            else
            {
                actionsLeft.color = new Color(1, 0, 0, .2f);
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

            if (enemies.Count == 0)
            {
                WinCondition();
            }
            if (playerUnits.Count == 0)
            {
                LoseCondition();
            }

        }

        public void WinCondition()
        {
            winCanvas.enabled = true;
            winCanvas.GetComponentInChildren<Animator>().SetTrigger("Win");
            hotbar.enabled = false;
            SaveAllUnits();
            GameScenes.BATTLEWIN = true;
            SceneManager.LoadSceneAsync(GameScenes.STRATEGYMAP);
        }

        public void LoseCondition()
        {
            loseCanvas.enabled = true;
            loseCanvas.GetComponentInChildren<Animator>().SetTrigger("Win");
            hotbar.enabled = false;
            SaveAllUnits();
            GameScenes.BATTLEWIN = false;
            SceneManager.LoadSceneAsync(GameScenes.STRATEGYMAP);
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
            foreach (GameObject obj in playerUnits)
            {
                obj.GetComponent<UniqueCreature>().myUnit.Save();
            }
        }
    }
}
