using SwordAndBored.GameData.Creatures;
using SwordAndBored.GameData.Roles;
using SwordAndBored.GameData.Units;
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

        private IUnit character;

        public void StageUnitForTraining()
        {
            // Create ICharacter to track in strategy view
            // Add record to database
            // Add to strategy view tracker

            character = new Unit(UnitRole);
            NameUnitCanvas.SetActive(true);
        }

        public void ConfirmUnitTraining()
        {
            Debug.Log("Triggered");
            character.Name = UnitName;
            Debug.Log(UnitName);
            UnitManager.Instance.RegisterUnit(character);
            NameUnitCanvas.gameObject.SetActive(false);
            character.Save();
        }

        public void CancelUnitTraining()
        {
            NameUnitCanvas.gameObject.SetActive(false);
        }
    }
}
