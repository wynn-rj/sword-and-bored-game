using System;
using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.StrategyView.BaseManagement.Cities
{
    public class SetActiveCity : MonoBehaviour
    {
        private CityEntry entry;
        private Action<GameObject> clickAction;

        private void Awake()
        {
            entry = GetComponent<CityEntryDisplay>().cityEntry;
        }

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(HandleButtonClick);
        }

        public void Initialize(Action<GameObject> clickAction)
        {
            this.clickAction = clickAction;
        }

        private void HandleButtonClick()
        {
            clickAction(gameObject);
        }
    }
}
