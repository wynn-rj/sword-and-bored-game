using SwordAndBored.GameData.Units;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.Strategy.BaseManagement.Units
{
    public class ShowEntryData : MonoBehaviour
    {
        private IUnit unit;
        private Action<IUnit> clickAction;

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(HandleButtonClick);
        }

        public void Initialize(IUnit unit, Action<IUnit> clickAction)
        {
            this.unit = unit;
            this.clickAction = clickAction;
        }

        private void HandleButtonClick()
        {
            clickAction(unit);
        }
    }
}
