using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SwordAndBored.SceneManagement;
using SwordAndBored.GameData.Equipment;

public class WinPanel : MonoBehaviour
{
    public TMP_Text lootText;
    public Button worldButton;
    public Button chestButton;

    public void ClickOnChest()
    {
        lootText.gameObject.SetActive(true);
        lootText.text = "You got " + RollRandomItem();
        chestButton.gameObject.SetActive(false);
        worldButton.gameObject.SetActive(true);
    }

    public void ClickWorldMap()
    {
        SceneManager.LoadSceneAsync(GameScenes.STRATEGYMAP);
    }

    public string RollRandomItem()
    {
        int itemType = Random.Range(0, 3);
        if (itemType == 0)
        {
            //Weapon
            int[] weaponID = { 1, 2, 3, 5, 6, 7, 8, 11, 12, 13, 15, 16, 17, 18, 19, 20, 21, 23, 24, 25, 26 };
            int itemNumber = Random.Range(0, weaponID.Length);
            IWeapon weapon = new Weapon(weaponID[itemNumber]);
            IInventoryItem itemWeapon = new InventoryItem(weapon);
            itemWeapon.SetQuantity(itemWeapon.Quantity + 1);
            return weapon.Name;
        } else if (itemType == 1)
        {
            // Spell Book
            int[] spellBookID = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            int itemNumber = Random.Range(0, spellBookID.Length);
            ISpellBook spellBook = new SpellBook(spellBookID[itemNumber]);
            IInventoryItem itemSpellBook = new InventoryItem(spellBook);
            itemSpellBook.SetQuantity(itemSpellBook.Quantity + 1);
            return spellBook.Name;
        } else
        {
            // Armor
            int[] armorID = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24};
            int itemNumber = Random.Range(0, armorID.Length);
            IArmor armor = new Armor(armorID[itemNumber]);
            IInventoryItem itemArmor = new InventoryItem(armor);
            itemArmor.SetQuantity(itemArmor.Quantity + 1);
            return armor.Name;
        }
    }
}
