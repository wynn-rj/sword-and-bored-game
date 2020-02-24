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
        public Text UnitRole;

        private Button trainButton;

        void Start()
        {
            trainButton = GetComponent<Button>();
            trainButton.onClick.AddListener(() => StageUnitForTraining());
        }

        void StageUnitForTraining()
        {    
            UnitFactory.UnitRole = UnitRole.text;
            UnitFactory.StageUnitForTraining();
        }
    }
}
