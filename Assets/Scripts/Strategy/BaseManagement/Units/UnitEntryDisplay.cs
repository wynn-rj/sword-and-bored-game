using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitEntryDisplay : MonoBehaviour
{
    public UnitEntry UnitEntry;

    public Image UnitImage;

    public Text UnitName;

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

    }

    
}
