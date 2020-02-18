using SwordAndBored.GameData.Creatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.StrategyView.BaseManagement
{
    public class UnitManager : MonoBehaviour
    {
        public IList<ICharacter> unitList;

        private void Awake()
        {
            unitList = new List<ICharacter>();
        }

        /// <summary>
        /// Retrieves all unit data from the database
        /// </summary>
        public void GetAllData()
        {

        }

        /// <summary>
        /// Writes all unit data to the database
        /// </summary>
        public void SetAllData()
        {

        }

        public void RegisterUnit()
        {

        }

        public IList<ICharacter> GetUnitsList()
        {
            return unitList;
        }
    }
}
