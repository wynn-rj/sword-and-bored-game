using SwordAndBored.GameData.Abilities.Skills;
using SwordAndBored.GameData.Creatures;
using SwordAndBored.GameData.Roles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitFactory : MonoBehaviour
{
    public Text UnitRole;

    public Text UnitName;

    private IDictionary<string, IRole> roleDict = new Dictionary<string, IRole>()
    {
        {"Warrior", new GenericRole(new SingleSelectSkillTree(5)) },
        {"Mage", new GenericRole(new SingleSelectSkillTree(5)) },
        {"Scout", new GenericRole(new SingleSelectSkillTree(5)) },
    };

    public void TrainUnit()
    {
        // Create ICharacter to track in strategy view
        // Add record to database
        // Add to strategy view tracker

        IRole role = roleDict[UnitRole.text];
        role.Name = UnitName.text;

        ICharacter character = new GenericCharacter(null, role, null);
    }
}
