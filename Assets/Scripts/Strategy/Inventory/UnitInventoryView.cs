using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SwordAndBored.GameData.Units;

namespace SwordAndBored.Strategy.Inventory
{
    public class UnitInventoryView : MonoBehaviour
    {
        public GameObject WeaponButton;
        public GameObject ArmorButton;
        public GameObject SpellbookButton;

        [SerializeField] private TextMeshProUGUI unitInfo;
        [SerializeField] private TextMeshProUGUI weaponInfo;
        [SerializeField] private TextMeshProUGUI armorInfo;
        [SerializeField] private TextMeshProUGUI spellbookInfo;
        [SerializeField] private IUnit currentUnit;
        private void Start()
        {
            unitInfo = GetComponent<TextMeshProUGUI>();
            weaponInfo = WeaponButton.GetComponent<TextMeshProUGUI>();
            armorInfo = ArmorButton.GetComponent<TextMeshProUGUI>();
            spellbookInfo = SpellbookButton.GetComponent<TextMeshProUGUI>();
        }

        public void SetUnit(IUnit unit)
        {
            weaponInfo.text = "No Weapon Equipped";
            armorInfo.text = "No Armor Equipped";
            spellbookInfo.text = "No Spellbook Equipped";
            currentUnit = unit;
            unitInfo.text = currentUnit.Name;
            if (currentUnit.Weapon != null) weaponInfo.text = currentUnit.Weapon.Name;
            if (currentUnit.Armor != null) armorInfo.text = currentUnit.Armor.Name;
            if (currentUnit.SpellBook != null) spellbookInfo.text = currentUnit.SpellBook.Name;
        }
    }
}
