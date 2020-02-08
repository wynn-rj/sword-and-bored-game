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
        public GameObject[] units;
        public AbstractTurnBrain activePlayer;
        [Header("UI")]
        TurnOrderController manager;
        public TextMeshProUGUI text;

        public Image[] actionsLeft;

        void Start()
        {
            manager = new TurnOrderController(units, new RandomShuffler<GameObject>());
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
        }
    }
}
