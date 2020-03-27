using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.GameData.Equipment;

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

        void Start()
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
                CreateButton(item);
            }

        }

        private void CreateButton(IInventoryItem item)
        {
            GameObject buttonObject = Instantiate(buttonTemplate) as GameObject;
            InventoryItemButton button = buttonObject.GetComponent<InventoryItemButton>();
            button.SetItem(item);
            buttonObject.transform.SetParent(buttonTemplate.transform.parent, false);
            buttonObject.SetActive(true);
        }

        public void EquipItem(string itemName)
        {
            Debug.Log(itemName + " : yeet");
        }
    }
}
