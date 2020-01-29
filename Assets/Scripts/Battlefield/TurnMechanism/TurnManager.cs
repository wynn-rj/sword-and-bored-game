using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.TurnMechanism;
using UnityEngine.UI;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public GameObject[] units;
    TurnOrderController manager;
    public UniqueCreature activePlayer;
    public TextMeshProUGUI text;

    public Image[] actionsLeft;

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
        activePlayer.StartTurn();
    }

    void Update()
    {
        if (activePlayer.action)
        {
            actionsLeft[0].color = new Color(0, 0, 1, .2f);
        }
        else
        {
            actionsLeft[0].color = new Color(1, 0, 0, .2f);
        }
    }
}
