using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.Strategy.BaseManagement.Units
{
    public class UnitEntryDisplay : MonoBehaviour
    {
        public UnitEntry UnitEntry;

        public Image UnitImage;
        public TMP_Text UnitName;
        public TMP_Text Squad;
        public TMP_Text Town;

        public List<Sprite> RoleSpriteList;

        public void SetDisplay()
        {
            IDictionary<string, Sprite> RoleImageDictionary = new Dictionary<string, Sprite>()
        {
            {"Warrior", RoleSpriteList[0]},
            {"Scout", RoleSpriteList[1]},
            {"Mage", RoleSpriteList[2]}
        };

            UnitImage.sprite = RoleImageDictionary[UnitEntry.Role];
            UnitName.text = UnitEntry.UnitName;

            Squad.text = "Squad: " + UnitEntry.CurrentSquad;
            Town.text = "City: " + UnitEntry.CurrentTown;

        }
    }
}
