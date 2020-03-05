using System;
using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.StrategyView.BaseManagement.Towns
{
    public class SetActiveTown : MonoBehaviour
    {
        private TownEntry entry;
        private Action<GameObject> clickAction;

        private void Awake()
        {
            entry = GetComponent<TownEntryDisplay>().townEntry;
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
