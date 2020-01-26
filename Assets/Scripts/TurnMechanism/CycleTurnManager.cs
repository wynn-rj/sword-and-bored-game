using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.TurnMechanism;
using UnityEngine.UI;
using TMPro;

namespace SwordAndBored.BattleMechanism
{
    public class CycleTurnManager : MonoBehaviour
    {
        public GameObject[] units;
        TurnOrderController manager;
        public UniqueCreature activePlayer;
        public TextMeshProUGUI text;

        void Start()
        {
            manager = new TurnOrderController(units, new RandomShuffler<GameObject>());
            activePlayer = manager.NextEntity().GetComponent<UniqueCreature>();
            text.text = "Current Player: " + activePlayer.gameObject.name;
        }

        public void nextTurn()
        {
            activePlayer = manager.NextEntity().GetComponent<UniqueCreature>();
            text.text = "Current Player: " + activePlayer.gameObject.name;
        }
    }
}
