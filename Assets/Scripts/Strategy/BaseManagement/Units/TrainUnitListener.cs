using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.Strategy.BaseManagement.Units
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
