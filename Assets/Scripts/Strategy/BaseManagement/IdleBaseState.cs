using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.StrategyView.BaseManagement
{
    public class IdleBaseState : AbstractBaseState
    {
        public IdleBaseState(BaseManager bm) : base(bm)
        {
            BaseManager.SetActiveCanvas(0);
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                BaseManager.ToggleActiveCanvas();
                BaseManager.BaseManagementState = new SelectBuildState(BaseManager);
            }

            base.Update();
        }
    }
}

