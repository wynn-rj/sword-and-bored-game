using SwordAndBored.GameData.Creatures;
using SwordAndBored.GameData.Units;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SwordAndBored.Strategy.BaseManagement
{
    public class UnitManager
    {
        public IList<IUnit> existingUnitList;
        public IList<IUnit> newUnitList;

        public static UnitManager Instance
        {
            get
            {
                return instance;
            }
        }

#pragma warning disable IDE0032
        private static readonly UnitManager instance = new UnitManager();
#pragma warning restore IDE0032

        private UnitManager()   
        {
            existingUnitList = new List<IUnit>();
            newUnitList = new List<IUnit>();
        }

        /// <summary>
        /// Retrieves all unit data from the database
        /// </summary>
        public void GetAllData()
        {
            /*
             * TO DO: Populate <>existingUnitList</> with data from database
             */
        }

        /// <summary>
        /// Writes all unit data to the database
        /// </summary>
        public void SetAllData()
        {
            /*
             * TO DO: Create database entries for new ICharacters
             */

            /*
             * TO DO: Update records of existing units
             */
        }

        public void RegisterUnit(IUnit character)
        {
            newUnitList.Add(character);
            //Debug.Log(character.Name);
        }

        public IList<IUnit> GetAllUnits()
        {
            return existingUnitList.Concat(newUnitList).ToList();
        }
    }
}
