using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.GameData.Equipment;

namespace SwordAndBored.Strategy.Inventory
{
    public class MonoInventory : MonoBehaviour 
    {
        public static MonoInventory Instance;

        private List<IInventoryItem> equipmentList = new List<IInventoryItem>();

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
            ReadInventoryFromDatabaseAndProcess();
        }

        private void ReadInventoryFromDatabaseAndProcess()
        {
            equipmentList = InventoryHelper.GetAllInventoryItemsWithOne();
        }

        void Update()
        {
            
        }
    }
}
