﻿using UnityEngine;
using SwordAndBored.GameData.Units;
using SwordAndBored.GameData.Roles;
using SwordAndBored.GameData.Equipment;
using TMPro;
using System.Collections.Generic;
using System.IO;
using System;

namespace SwordAndBored.GameData.Database
{
    public class UnitFromDatabase : MonoBehaviour
    {
        public TMP_Text textBox;
        private DatabaseConnection conn;
        // Start is called before the first frame update
        void Start()
        {
            //conn = new DatabaseConnection();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                List<IUnit> all = Unit.GetAllUnits();
                foreach (IUnit unit in all)
                {
                    textBox.text += unit.ToString();
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                IEnemy enemy = new Enemy(1);
                textBox.text = enemy.ToString();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                ISquad squad = new Squad(0, 0);
                foreach (IUnit unit in squad.Units)
                {
                    textBox.text += unit.ToString();
                }
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                IUnit unit = new Unit(30);
                textBox.text = unit.Squad.Name;
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                List<IInventoryItem> items = InventoryHelper.GetAllInventoryItems();
                string itemText = "";
                foreach (IInventoryItem item in items)
                {
                    itemText += $"ID: {item.ID} ";
                }
                textBox.text = itemText;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                List<IInventoryItem> items = InventoryHelper.GetWeapons();
                string itemText = "";
                foreach (IInventoryItem item in items)
                {
                    itemText += $"ID: {item.ID} ";
                }
                textBox.text = itemText;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                List<IInventoryItem> items = InventoryHelper.GetWeaponsWithOne();
                string itemText = "";
                foreach (IInventoryItem item in items)
                {
                    itemText += $"ID: {item.ID} ";
                }
                textBox.text = itemText;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                string nameText = "";
                foreach (string name in DatabaseManager.GetPreviousSaveNames())
                {
                    nameText += name + "\n";
                }
                textBox.text = nameText;
            }
        }
    }

}
