using SwordAndBored.GameData.Units;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.Strategy.BaseManagement.Units
{
    public class UnitEntryDisplay : MonoBehaviour
    {
        public UnitEntry unitEntry;

        public Image unitImage;
        public TMP_Text unitName;
        public TMP_Text squad;
        public TMP_Text town;

        public List<Sprite> roleSpriteList;

        public void SetDisplay()
        {
            IDictionary<string, Sprite> roleImageDictionary = new Dictionary<string, Sprite>()
            {
                {"Warrior", roleSpriteList[0]},
                {"Scout", roleSpriteList[1]},
                {"Mage", roleSpriteList[2]}
            };

            unitImage.sprite = roleImageDictionary[unitEntry.role];
            unitName.text = unitEntry.unitName;

            squad.text = "Squad: " + unitEntry.currentSquad;
            town.text = "Town: " + unitEntry.currentTown;

        }

        public void UpdateDisplay(IUnit unit)
        {
            unitEntry.Init(unit);
            SetDisplay();
        }
    }
}
