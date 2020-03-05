using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.BaseManagement.Units;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUnitToSquad : MonoBehaviour
{
    private Action<IUnit> addAction;
    private Action<IUnit> removeAction;

    private bool added;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(HandleButtonClick);
        added = false;
    }

    public void Initialize(Action<IUnit> addAction, Action<IUnit> removeAction)
    {
        this.addAction = addAction;
        this.removeAction = removeAction;
    }

    private void HandleButtonClick()
    {
        IUnit unit = gameObject.GetComponent<UnitEntryDisplay>().unitEntry.unit;
        if (!added)
        {
            addAction(unit);
            added = true;
            GetComponent<Button>().image.color = Color.red;
        }
        else
        {
            removeAction(unit);
            added = false;
            GetComponent<Button>().image.color = Color.white;
        }
    }
}
