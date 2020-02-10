using SwordAndBored.GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.StrategyView.BaseManagement.Buildings
{
    public interface IBuilding : IModelable, IDescriptable
    {
        Canvas HUD { get; set; }

        int Tier { get; set; }

        float BuildTime { get; set; }

        string ShadowModelName { get; set; }

    }
}
