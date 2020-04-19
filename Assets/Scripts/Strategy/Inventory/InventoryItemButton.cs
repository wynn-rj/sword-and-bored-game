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
        private enum ItemType { NONE, ARMOR, WEAPON, SPELLBOOK };
        private ItemType itemType;
        private string itemName = "default name";


        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        public void SetItem(IInventoryItem item)
        {
            this.item = item;
            int quant = item.Quantity;
            if (item.Weapon != null)
            {
                itemName = item.Weapon.Name;
                itemType = ItemType.WEAPON;
            }
            else if (item.Armor != null)
            {
                itemName = item.Armor.Name;
                itemType = ItemType.ARMOR;
            }
            else if (item.SpellBook != null)
            {
                itemName = item.SpellBook.Name;
                itemType = ItemType.SPELLBOOK;
            }

            textMesh.text = $"{itemType} : {itemName} : {quant}";
        }

        public void OnClick()
        {
            switch(itemType)
            {
                case ItemType.ARMOR:
                    inventory.EquipArmor(item);
                    break;
                case ItemType.WEAPON:
                    inventory.EquipWeapon(item);
                    break;
                case ItemType.SPELLBOOK:
                    inventory.EquipSpellbook(item);
                    break;
                default:
                    Debug.Log("what in the Yeet is this item!");
                    break;
            }
        }
    }
}
