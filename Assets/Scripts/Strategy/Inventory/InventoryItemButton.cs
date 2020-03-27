using UnityEngine;
using SwordAndBored.GameData.Equipment;
using TMPro;


namespace SwordAndBored.Strategy.Inventory
{
    public class InventoryItemButton : MonoBehaviour
    {
        public TextMeshProUGUI textMesh;
        public MonoInventory inventory;
        public IInventoryItem item;

        public void SetText(IInventoryItem item)
        {
            string name = "";
            int quant = item.Quantity;
            if(item.Weapon != null) name = item.Weapon.Name;
            else if(item.Armor != null) name = item.Armor.Name;
            else if(item.SpellBook != null)  name = item.SpellBook.Name;
            textMesh.text = "{name} : {quant}";
        }

        public void OnClick()
        {
            inventory.EquipItem(item);
        }
    }
}
