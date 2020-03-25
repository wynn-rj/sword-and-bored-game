using SwordAndBored.GameData.Database;

namespace SwordAndBored.GameData.Equipment
{
    public class InventoryItem : IInventoryItem
    {
        public IWeapon Weapon { get; }
        public IArmor Armor { get; }
        public ISpellBook SpellBook { get; }
        public int Quantity { get; }
        public int ID { get; set; }

        public InventoryItem(IWeapon weapon)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWhereColNameEqualsInt("Inventory", "Weapon_FK", weapon.ID);

            Weapon = weapon;
            if (reader.NextRow())
            {
                ID = reader.GetIntFromCol("ID");
                Quantity = reader.GetIntFromCol("Quantity");
            }
            else
            {
                string query = $"INSERT INTO Inventory (Weapon_FK, Quantity) VALUES ({weapon.ID}, 0);";
                conn.ExecuteNonQuery(query);
                Quantity = 0;
                reader.CloseReader();
                reader = conn.QueryRowFromTableWhereColNameEqualsInt("Inventory", "Weapon_FK", weapon.ID);
                ID = reader.GetIntFromCol("ID");
            }
            reader.CloseReader();
            conn.CloseConnection();
        }

        public InventoryItem(IArmor armor)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWhereColNameEqualsInt("Inventory", "Armor_FK", armor.ID);

            Armor = armor;
            if (reader.NextRow())
            {
                ID = reader.GetIntFromCol("ID");
                Quantity = reader.GetIntFromCol("Quantity");
            }
            else
            {
                string query = $"INSERT INTO Inventory (Armor_FK, Quantity) VALUES ({armor.ID}, 0);";
                conn.ExecuteNonQuery(query);
                Quantity = 0;
                reader.CloseReader();
                reader = conn.QueryRowFromTableWhereColNameEqualsInt("Inventory", "Armor_FK", armor.ID);
                ID = reader.GetIntFromCol("ID");
            }
            reader.CloseReader();
            conn.CloseConnection();
        }

        public InventoryItem(ISpellBook spellBook)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWhereColNameEqualsInt("Inventory", "Spell_Book_FK", spellBook.ID);

            SpellBook = spellBook;
            if (reader.NextRow())
            {
                ID = reader.GetIntFromCol("ID");
                Quantity = reader.GetIntFromCol("Quantity");
            }
            else
            {
                string query = $"INSERT INTO Inventory (Spell_Book_FK, Quantity) VALUES ({spellBook.ID}, 0);";
                conn.ExecuteNonQuery(query);
                Quantity = 0;
                reader.CloseReader();
                reader = conn.QueryRowFromTableWhereColNameEqualsInt("Inventory", "Spell_Book_FK", spellBook.ID);
                ID = reader.GetIntFromCol("ID");
            }
            reader.CloseReader();
            conn.CloseConnection();
        }

        public InventoryItem(int inputID)
        {
            DatabaseConnection conn = new DatabaseConnection();
            DatabaseReader reader = conn.QueryRowFromTableWithID("Inventory", inputID);

            ID = inputID;
            if (reader.NextRow())
            {
                Quantity = reader.GetIntFromCol("Quantity");
                int weaponID = reader.GetIntFromCol("Weapon_FK");
                int armorID = reader.GetIntFromCol("Armor_FK");
                int spellBookID = reader.GetIntFromCol("Spell_Book_FK");
                if (weaponID >= 0)
                {
                    Weapon = new Weapon(weaponID);
                }
                else if (armorID >= 0)
                {
                    Armor = new Armor(armorID);
                }
                else if (spellBookID >= 0)
                {
                    SpellBook = new SpellBook(spellBookID);
                }
            }
            reader.CloseReader();
            conn.CloseConnection();
        }

        public InventoryItem(int newID, IWeapon weapon, IArmor armor, ISpellBook spellBook, int quantity)
        {
            ID = newID;
            Weapon = weapon;
            Armor = armor;
            SpellBook = spellBook;
            Quantity = quantity;
        }

        public void SetQuantity(int newQuantity)
        {
            DatabaseConnection conn = new DatabaseConnection();
            string query = $"UPDATE Inventory SET Quantity = {newQuantity} WHERE ID = {ID};";
            conn.ExecuteNonQuery(query);
            conn.CloseConnection();
        }
    }
}
