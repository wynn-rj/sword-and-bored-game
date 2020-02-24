using SwordAndBored.StrategyView.BaseManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.Strategy.BaseManagement
{
    public class TrainUnitListener : MonoBehaviour
    {
        public UnitFactory UnitFactory;
        public Button confirmButton;
        public Text UnitRole;
        public InputField UnitName;

        private Button trainButton;

        void Start()
        {
            trainButton = GetComponent<Button>();
            trainButton.onClick.AddListener(() => StageUnitForTraining());

            confirmButton.onClick.AddListener(() => ConfirmUnitTraining());
        }

        void StageUnitForTraining()
        {    
            UnitFactory.UnitRole = UnitRole.text;
            UnitFactory.StageUnitForTraining();
        }

        void ConfirmUnitTraining()
        {
            UnitFactory.UnitName = UnitName.text;
            UnitFactory.ConfirmUnitTraining();
            UnitName.text = "";
        }
    }
}
