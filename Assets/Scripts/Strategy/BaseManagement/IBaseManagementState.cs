using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseManagementState : IUpdatable
{
    void ToggleBuildingsList();

    void SelectBuilding();

    void PlaceBuilding();
}
