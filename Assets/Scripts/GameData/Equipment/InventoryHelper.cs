using System.Collections.Generic;
using SwordAndBored.GameData.Database;

namespace SwordAndBored.GameData.Equipment
{
    public static class InventoryHelper
    {
        public static List<IInventoryItem> GetAllInventoryItems()
        {
            List<IInventoryItem> items = new List<IInventoryItem>();
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryAllFromTable("Inventory");
            while (reader.NextRow())
            {
                int currentID = reader.GetIntFromCol("ID");
                items.Add(new InventoryItem(currentID));
            }

            reader.CloseReader();
            conn.CloseConnection();

            return items;
        }

        public static List<IInventoryItem> GetAllInventoryItemsWithOne()
        {
            List<IInventoryItem> items = new List<IInventoryItem>();
            DatabaseConnection conn = new DatabaseConnection();
            string query = "SELECT * FROM Inventory WHERE Quantity > 0;";
            DatabaseReader reader = conn.ExecuteQuery(query);
            while (reader.NextRow())
            {
                int currentID = reader.GetIntFromCol("ID");
                items.Add(new InventoryItem(currentID));
            }

            reader.CloseReader();
            conn.CloseConnection();

            return items;
        }

        public static List<IInventoryItem> GetWeapons()
        {
            List<IInventoryItem> items = new List<IInventoryItem>();
            DatabaseConnection conn = new DatabaseConnection();
            string query = "SELECT * FROM Inventory WHERE Weapon_FK > 0;";
            DatabaseReader reader = conn.ExecuteQuery(query);
            while (reader.NextRow())
            {
                int currentID = reader.GetIntFromCol("ID");
                items.Add(new InventoryItem(currentID));
            }

            reader.CloseReader();
            conn.CloseConnection();

            return items;
        }

        public static List<IInventoryItem> GetWeaponsWithOne()
        {
            List<IInventoryItem> items = new List<IInventoryItem>();
            DatabaseConnection conn = new DatabaseConnection();
            string query = "SELECT * FROM Inventory WHERE Quantity > 0 AND Weapon_FK > 0;";
            DatabaseReader reader = conn.ExecuteQuery(query);
            while (reader.NextRow())
            {
                int currentID = reader.GetIntFromCol("ID");
                items.Add(new InventoryItem(currentID));
            }

            reader.CloseReader();
            conn.CloseConnection();

            return items;
        }

        public static List<IInventoryItem> GetArmors()
        {
            List<IInventoryItem> items = new List<IInventoryItem>();
            DatabaseConnection conn = new DatabaseConnection();
            string query = "SELECT * FROM Inventory WHERE Armor_FK > 0;";
            DatabaseReader reader = conn.ExecuteQuery(query);
            while (reader.NextRow())
            {
                int currentID = reader.GetIntFromCol("ID");
                items.Add(new InventoryItem(currentID));
            }

            reader.CloseReader();
            conn.CloseConnection();

            return items;
        }

        public static List<IInventoryItem> GetArmorsWithOne()
        {
            List<IInventoryItem> items = new List<IInventoryItem>();
            DatabaseConnection conn = new DatabaseConnection();
            string query = "SELECT * FROM Inventory WHERE Quantity > 0 AND Armor_FK > 0;";
            DatabaseReader reader = conn.ExecuteQuery(query);
            while (reader.NextRow())
            {
                int currentID = reader.GetIntFromCol("ID");
                items.Add(new InventoryItem(currentID));
            }

            reader.CloseReader();
            conn.CloseConnection();

            return items;
        }

        public static List<IInventoryItem> GetSpellBooks()
        {
            List<IInventoryItem> items = new List<IInventoryItem>();
            DatabaseConnection conn = new DatabaseConnection();
            string query = "SELECT * FROM Inventory WHERE Spell_Book_FK > 0;";
            DatabaseReader reader = conn.ExecuteQuery(query);
            while (reader.NextRow())
            {
                int currentID = reader.GetIntFromCol("ID");
                items.Add(new InventoryItem(currentID));
            }

            reader.CloseReader();
            conn.CloseConnection();

            return items;
        }

        public static List<IInventoryItem> GetSpellBooksWithOne()
        {
            List<IInventoryItem> items = new List<IInventoryItem>();
            DatabaseConnection conn = new DatabaseConnection();
            string query = "SELECT * FROM Inventory WHERE Quantity > 0 AND Spell_Book_FK > 0;";
            DatabaseReader reader = conn.ExecuteQuery(query);
            while (reader.NextRow())
            {
                int currentID = reader.GetIntFromCol("ID");
                items.Add(new InventoryItem(currentID));
            }

            reader.CloseReader();
            conn.CloseConnection();

            return items;
        }
    }
}
