using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseManagementState : IUpdatable
{
    void SelectBuildingTier();

    void SelectBuilding();

    void PlaceBuilding();
}
