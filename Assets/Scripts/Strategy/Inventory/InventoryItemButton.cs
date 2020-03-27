using UnityEngine;
using UnityEngine.UI;
using SwordAndBored.GameData.Equipment;
using TMPro;


namespace SwordAndBored.Strategy.Inventory
{
    public class InventoryItemButton : MonoBehaviour
    {
        public TextMeshProUGUI textMesh;
        public MonoInventory inventory;
        public IInventoryItem item;
        private string itemName = "default name";

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        public void SetItem(IInventoryItem item)
        {
            this.item = item;
            int quant = item.Quantity;
            if(item.Weapon != null) itemName = item.Weapon.Name;
            else if(item.Armor != null) itemName = item.Armor.Name;
            else if(item.SpellBook != null)  itemName = item.SpellBook.Name;
            textMesh.text = $"{itemName} : {quant}";
        }

        public void OnClick()
        {
            inventory.EquipItem(itemName);
        }
    }
}
