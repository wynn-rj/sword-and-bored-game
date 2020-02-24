using SwordAndBored.GameData.Abilities.Skills;
using SwordAndBored.GameData.Creatures;
using SwordAndBored.GameData.Roles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.Strategy.BaseManagement
{
    public class UnitFactory : MonoBehaviour
    {
        public string UnitRole { get; set; }
        public string UnitName { get; set; }

        public GameObject NameUnitCanvas;

        private ICharacter character;

        private IDictionary<string, IRole> roleDict = new Dictionary<string, IRole>()
        {
            /*
            * TO DO: specificy skill tree arguments
            */
            {"Warrior", new GenericRole(new SingleSelectSkillTree(5)) },
            {"Mage", new GenericRole(new SingleSelectSkillTree(5)) },
            {"Scout", new GenericRole(new SingleSelectSkillTree(5)) },
        };

        public void StageUnitForTraining()
        {
            // Create ICharacter to track in strategy view
            // Add record to database
            // Add to strategy view tracker

            IRole role = roleDict[UnitRole];
            role.Name = UnitRole;
            character = new GenericCharacter(null, role, null);
            NameUnitCanvas.SetActive(true);
        }

        public void ConfirmUnitTraining()
        {
            character.Name = UnitName;
            UnitManager.Instance.RegisterUnit(character);
            NameUnitCanvas.gameObject.SetActive(false);
        }

        public void CancelUnitTraining()
        {
            NameUnitCanvas.gameObject.SetActive(false);
        }
    }
}
