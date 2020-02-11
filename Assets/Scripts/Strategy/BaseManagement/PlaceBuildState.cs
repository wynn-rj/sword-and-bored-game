using SwordAndBored.StrategyView.BaseManagement.Buildings;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.StrategyView.BaseManagement
{
    public class PlaceBuildState : AbstractBaseState
    {
        private Vector3 position;
        private RaycastHit hit;
        private Ray ray;

        IBuilding building;
        GameObject shadowModel;

        public PlaceBuildState(BaseManager bm, IBuilding building) : base(bm)
        {
            BaseManager.ToggleActiveCanvas();
            this.building = building;
            //Instantiate behind camera out of view for now
            this.shadowModel = BaseManager.Instantiate(Resources.Load("Buildings/" + building.ShadowModelName) as GameObject, new Vector3(0, 11, -11), Quaternion.identity);
        }

        public override void PlaceBuilding()
        {
            BaseManager.Destroy(shadowModel);
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = BaseManager.BaseGrid.ReturnGridPoint(position);
            BaseManager.BaseManagementState = new IdleBaseState(BaseManager);

            base.PlaceBuilding();
        }

        public override void Update()
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 50))
            {
                position = hit.point;
                shadowModel.transform.position = BaseManager.BaseGrid.ReturnGridPoint(position);
            }

            if (Input.GetMouseButtonDown(0))
            {
                PlaceBuilding();
            }

            base.Update();
        }
    }
}

