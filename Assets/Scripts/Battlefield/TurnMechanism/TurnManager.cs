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
            activePlayer = manager.NextEntity().GetComponent<BrainManager>();
            text.text = "Current Player: " + activePlayer.GetName();
            activePlayer.isMyTurn = true;

            // Reset Turn Behaviors
            activePlayer.GetComponent<UniqueCreature>().movementLeft = activePlayer.GetComponent<UniqueCreature>().stats.movement;
            activePlayer.GetComponent<UniqueCreature>().action = true;
        }

        void Update()
        {
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
            GameScenes.BATTLEWIN = true;
            SceneManager.LoadSceneAsync(GameScenes.STRATEGYMAP);
        }

        public void LoseCondition()
        {
            loseCanvas.enabled = true;
            loseCanvas.GetComponentInChildren<Animator>().SetTrigger("Win");
            hotbar.enabled = false;
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
    }
}
