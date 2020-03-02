using SwordAndBored.Strategy.BaseManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmUnitListener : MonoBehaviour
{
    public UnitFactory UnitFactory;
    public InputField UnitName;

    private Button confirmButton;

    void Start()
    {
        confirmButton = GetComponent<Button>();
        confirmButton.onClick.AddListener(() => ConfirmUnitTraining());
    }

    void ConfirmUnitTraining()
    {
        UnitFactory.UnitName = UnitName.text;
        UnitFactory.ConfirmUnitTraining();
        UnitName.text = "";
    }
}
