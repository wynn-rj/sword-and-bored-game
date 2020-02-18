using SwordAndBored.GameData.Creatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.StrategyView.BaseManagement.Buildings
{
    public class Barracks : AbstractProductionBuilding
    {
        private IList<ICharacter> buildableUnits;

        public Barracks() : base()
        {
            ModelName = "Barracks";
        }

        public void BuildUnit()
        { 
        
        }
    }
}
