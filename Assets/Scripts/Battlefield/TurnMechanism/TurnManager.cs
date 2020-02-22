﻿using System.Collections;
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
        public AbstractTurnBrain activePlayer;
        [Header("UI")]
        TurnOrderController manager;
        public TextMeshProUGUI text;

        public Image[] actionsLeft;
        [HideInInspector]
        public List<GameObject> enemies = new List<GameObject>();

        public Canvas winCanvas;

        void Start()
        {
            manager = new TurnOrderController(units.ToArray(), new RandomShuffler<GameObject>());
            activePlayer = manager.NextEntity().GetComponent<AbstractTurnBrain>();
            text.text = "Current Player: " + activePlayer.GetName();
            activePlayer.DoTurn();
        }

        public void nextTurn()
        {
            activePlayer = manager.NextEntity().GetComponent<AbstractTurnBrain>();
            text.text = "Current Player: " + activePlayer.GetName();
            activePlayer.DoTurn();
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
            winCanvas.GetComponentInChildren<Animator>().SetTrigger("Win");
            Debug.Log("You Win!");
        }
    }
}
