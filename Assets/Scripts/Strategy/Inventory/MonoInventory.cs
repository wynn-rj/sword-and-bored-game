﻿using System.Collections.Generic;
using UnityEngine;
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
        [SerializeField] private UnitEquipmentView unitEquipmentView;

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

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void OpenInventory(IUnit unit)
        {
            CurrentUnit = unit;
            gameObject.SetActive(true);
            unitEquipmentView.gameObject.SetActive(true);
            DestroyAllButtons();
            ReadInventoryFromDatabaseAndProcess();
        }

        public void CloseInventory()
        {
            DestroyAllButtons();
            unitEquipmentView.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        private void ReadInventoryFromDatabaseAndProcess()
        {
            equipmentList = InventoryHelper.GetAllInventoryItemsWithOne();
            foreach(IInventoryItem item in equipmentList)
            {
                CreateButton(item);
            }

        }

        private void DestroyAllButtons()
        {
            foreach(GameObject button in buttons)
            {
                Destroy(button);
            }
        }

        private void CreateButton(IInventoryItem item)
        {
            GameObject buttonObject = Instantiate(buttonTemplate) as GameObject;
            buttons.Add(buttonObject);
            InventoryItemButton button = buttonObject.GetComponent<InventoryItemButton>();
            button.SetItem(item);
            buttonObject.transform.SetParent(buttonTemplate.transform.parent, false);
            buttonObject.SetActive(true);
        }

        public void EquipArmor(IInventoryItem itemName)
        {
            Debug.Log(itemName + " : ARMOR");
            if(CurrentUnit != null && CurrentUnit.Armor != null)
            {
                InventoryItem unitEquipment = new InventoryItem(CurrentUnit.Armor);
                unitEquipment.SetQuantity(unitEquipment.Quantity + 1);
                CurrentUnit.Armor = null;
            }

            InventoryItem item = itemName as InventoryItem;
            if(item != null)
            {
                CurrentUnit.Armor = item.Armor;
                item.SetQuantity(item.Quantity - 1);
            }
            CurrentUnit.Save();
            OpenInventory(CurrentUnit);
        }

        public void EquipWeapon(IInventoryItem itemName)
        {
            Debug.Log(itemName + " : WEAPON");
            if(CurrentUnit != null && CurrentUnit.Weapon != null)
            {
                InventoryItem unitEquipment = new InventoryItem(CurrentUnit.Weapon);
                unitEquipment.SetQuantity(unitEquipment.Quantity + 1);
                CurrentUnit.Weapon = null;
            }

            InventoryItem item = itemName as InventoryItem;
            if(item != null)
            {
                CurrentUnit.Weapon = item.Weapon;
                item.SetQuantity(item.Quantity - 1);
            }
            CurrentUnit.Save();
            OpenInventory(CurrentUnit);
        }

        public void EquipSpellbook(IInventoryItem itemName)
        {
            Debug.Log(itemName + " : SPELLBOOK");
            if(CurrentUnit != null && CurrentUnit.SpellBook != null)
            {
                InventoryItem unitEquipment = new InventoryItem(CurrentUnit.SpellBook);
                unitEquipment.SetQuantity(unitEquipment.Quantity + 1);
                CurrentUnit.SpellBook = null;
            }

            InventoryItem item = itemName as InventoryItem;
            if(item != null)
            {
                CurrentUnit.SpellBook = item.SpellBook;
                item.SetQuantity(item.Quantity - 1);
            }
            CurrentUnit.Save();
            OpenInventory(CurrentUnit);
        }

    }
}
