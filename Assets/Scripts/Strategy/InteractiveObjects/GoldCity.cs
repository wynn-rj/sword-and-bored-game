using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Strategy.ProceduralTerrain;
using SwordAndBored.Strategy.GameResources;

namespace SwordAndBored.Strategy.InteractiveObjects
{
    public class GoldCity : MonoBehaviour
    {
        public Gold gold;

        public void addGold()
        {
            gold.Amount += 100;
        }
    }
}
