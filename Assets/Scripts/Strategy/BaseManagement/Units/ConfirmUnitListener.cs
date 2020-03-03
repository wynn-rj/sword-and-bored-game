using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.Strategy.BaseManagement.Units
{
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
}
