using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.GameData.Equipment;
using SwordAndBored.GameData;

namespace SwordAndBored.Strategy.Inventory
{
    public class MonoInventory : MonoBehaviour
    {
        public static MonoInventory Instance;

        private List<IInventoryItem> equipmentList = new List<IInventoryItem>();
        private List<GameObject> buttons = new List<GameObject>();

        [SerializeField] private GameObject buttonTemplate;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                Debug.LogWarning("Only one MonoInventory is allowed per scene!");
            }
        }

        void OnEnable()
        {
            OpenInventory();
        }

        public void OpenInventory()
        {
            foreach(GameObject button in buttons)
            {
                Destroy(button);
            }
            ReadInventoryFromDatabaseAndProcess();
        }

        private void ReadInventoryFromDatabaseAndProcess()
        {
            equipmentList = InventoryHelper.GetAllInventoryItemsWithOne();
            foreach(IInventoryItem item in equipmentList)
            {
            }

        }

        private void CreateButton(IDescriptable desc, int quantity)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }

        public void EquipItem(IInventoryItem item)
        {
            Debug.Log(item.Quantity);
        }
    }
}
