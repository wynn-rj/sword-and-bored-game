using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SwordAndBored.GameData.Equipment;
using SwordAndBored.GameData.Units;

namespace SwordAndBored.Strategy.Inventory
{
    public class MonoInventory : MonoBehaviour
    {
        public static MonoInventory Instance;

        public IUnit CurrentUnit;

        private List<IInventoryItem> equipmentList = new List<IInventoryItem>();
        private List<GameObject> buttons = new List<GameObject>();


        [SerializeField] private GameObject buttonTemplate;
        [SerializeField] private GameObject unitInventoryView;
        [SerializeField] private UnitEquipmentView unitInventory;

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

        public void OpenInventory(IUnit unit)
        {
            CurrentUnit = unit;
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

        public void EquipArmor(IInventoryItem itemName)
        {
            Debug.Log(itemName + " : ARMOR");
            OpenInventory(CurrentUnit);
        }

        public void EquipWeapon(IInventoryItem item)
        {
            Debug.Log(item + " : WEAPON");
            OpenInventory(CurrentUnit);
        }

        public void EquipSpellbook(IInventoryItem item)
        {
            Debug.Log(item + " : SPELLBOOK");
            OpenInventory(CurrentUnit);
        }

    }
}
