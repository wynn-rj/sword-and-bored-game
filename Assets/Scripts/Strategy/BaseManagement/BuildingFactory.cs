using SwordAndBored.StrategyView.BaseManagement.Buildings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.StrategyView.BaseManagement
{
    public static class BuildingFactory
    {
        public static IBuilding CreateBarracks()
        {
            return new Barracks();
        }

        public static IBuilding CreateGranary()
        {
            return null;
        }
    }
}
