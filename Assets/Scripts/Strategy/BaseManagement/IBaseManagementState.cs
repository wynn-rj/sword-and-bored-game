using SwordAndBored.GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.StrategyView.BaseManagement
{
    public interface IBaseManagementState : IUpdatable
    {
        void SelectBuildingTier();

        void SelectBuilding();

        void PlaceBuilding();

        void Exit();
    }
}
