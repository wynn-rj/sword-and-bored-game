using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SwordAndBored.GameData.Units;

namespace SwordAndBored.Strategy.Inventory
{
    public class UnitEquipmentView : MonoBehaviour
    {
        [SerializeField]private MonoInventory inventoryMaster;
        [SerializeField] private TextMeshProUGUI unitName;
        [SerializeField] private TextMeshProUGUI weaponInfo;
        [SerializeField] private TextMeshProUGUI armorInfo;
        [SerializeField] private TextMeshProUGUI spellbookInfo;
        [SerializeField] private IUnit currentUnit;

        public void Update()
        {
            currentUnit = inventoryMaster.CurrentUnit;
            if(currentUnit != null)
            {
                unitName.text = currentUnit.Name;

                if (currentUnit.Weapon != null && currentUnit.Weapon.Name != null)
                {
                    weaponInfo.text = currentUnit.Weapon.Name;
                }
                else
                {
                    weaponInfo.text = "No Weapon Equipped";
                }
                if (currentUnit.Armor != null && currentUnit.Armor.Name != null)
                {
                    armorInfo.text = currentUnit.Armor.Name;
                }
                else
                {
                    armorInfo.text = "No Armor Equipped";
                }
                if (currentUnit.SpellBook != null && currentUnit.SpellBook.Name != null)
                {
                    spellbookInfo.text = currentUnit.SpellBook.Name;
                }
                else
                {
                    spellbookInfo.text = "No Spellbook Equipped";
                }
            }
            else
            {
                unitName.text = "No Unit Set";
                spellbookInfo.text = "No Unit Set";
                armorInfo.text = "No Unit Set";
                weaponInfo.text = "No Unit Set";
            }
        }

        public void SetUnit(IUnit unit)
        {
            currentUnit = unit;
        }
    }
}
