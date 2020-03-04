using System;
using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.Strategy.BaseManagement.Units
{
    public class SetActiveEntry : MonoBehaviour
    {
        private UnitEntry entry;
        private Action<GameObject> clickAction;

        private void Awake()
        {
            entry = GetComponent<UnitEntryDisplay>().unitEntry;
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
