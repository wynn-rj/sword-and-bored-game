using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Battlefield.TurnMechanism;
using UnityEngine.UI;
using TMPro;
using SwordAndBored.Battlefield.CreaturScripts;

namespace SwordAndBored.Battlefield.TurnMechanism
{
    public class TurnManager : MonoBehaviour
    {
        [Header("Units")]
        public List<GameObject> units = new List<GameObject>();
        public List<GameObject> playerUnits = new List<GameObject>();
        public BrainManager activePlayer;
        [Header("UI")]
        TurnOrderController manager;
        public TextMeshProUGUI text;

        public Image[] actionsLeft;
        [HideInInspector]
        public List<GameObject> enemies = new List<GameObject>();

        public Canvas winCanvas;

        public Canvas hotbar;

        void Start()
        {
            winCanvas.enabled = false;
            manager = new TurnOrderController(units.ToArray(), new RandomShuffler<GameObject>());
            activePlayer = manager.NextEntity().GetComponent<BrainManager>();
            text.text = "Current Player: " + activePlayer.GetName();
            activePlayer.isMyTurn = true;
        }

        public void nextTurn()
        {
            activePlayer = manager.NextEntity().GetComponent<BrainManager>();
            text.text = "Current Player: " + activePlayer.GetName();
            activePlayer.isMyTurn = true;
        }

        void Update()
        {
            if (activePlayer && !activePlayer.GetTurnEnd())
            {
                nextTurn();
            }

            if (activePlayer.HasActionLeft())
            {
                actionsLeft[0].color = new Color(0, 0, 1, .2f);
            }
            else
            {
                actionsLeft[0].color = new Color(1, 0, 0, .2f);
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

        }

        public void WinCondition()
        {
            winCanvas.enabled = true;
            winCanvas.GetComponentInChildren<Animator>().SetTrigger("Win");
            hotbar.enabled = false;
            Debug.Log("You Win!");
        }
    }
}
