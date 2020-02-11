using SwordAndBored.StrategyView.BaseManagement.Buildings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.StrategyView.BaseManagement
{
    public class SelectBuildState : AbstractBaseState
    {
        public SelectBuildState(BaseManager bm) : base(bm) { }

        public override void SelectBuilding()
        {
            AbstractBuilding selectedBuilding = BaseManager.GetBuilding(BaseManager.ActiveTier) as AbstractBuilding;
            BaseManager.BaseManagementState = new PlaceBuildState(BaseManager, selectedBuilding);

            base.SelectBuilding();
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                BaseManager.ToggleActiveCanvas();
                BaseManager.BaseManagementState = new IdleBaseState(BaseManager);
            }
        }
    }
}

