using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.BaseManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.StrategyView.BaseManagement.Buildings
{
    public class Barracks : GenericStrongholdCell
    {
        private IList<IUnit> buildableUnits;

        private void Awake()
        {
            GameObject newCanvas = Instantiate(Canvas, Vector3.zero, Quaternion.identity);
            SetCanvas(newCanvas);
            Canvas.gameObject.SetActive(false);     
        }
    }
}
