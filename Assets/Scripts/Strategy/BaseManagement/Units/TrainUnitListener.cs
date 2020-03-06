using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.Strategy.BaseManagement.Units
{
    public class TrainUnitListener : MonoBehaviour
    {
        public UnitFactory unitFactory;
        public Text unitRole;

        private Button trainButton;

        void Start()
        {
            trainButton = GetComponent<Button>();
            trainButton.onClick.AddListener(() => StageUnitForTraining());
        }

        void StageUnitForTraining()
        {    
            unitFactory.UnitRole = unitRole.text;
            unitFactory.StageUnitForTraining();
        }
    }
}
