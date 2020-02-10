using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.StrategyView.BaseManagement
{
    public class AbstractBaseState : IBaseManagementState
    {
        protected BaseManager BaseManager { get; set; }

        public AbstractBaseState(BaseManager bm)
        {
            this.BaseManager = bm;
        }

        public virtual void Update() { }

        public virtual void SelectBuildingTier() { }

        public virtual void SelectBuilding() { }

        public virtual void PlaceBuilding() { }

        public virtual void Exit()
        {
            BaseManager.SetAllCanvasInactive();
            BaseManager.BaseManagementState = new IdleBaseState(BaseManager);
        }
    }
}

