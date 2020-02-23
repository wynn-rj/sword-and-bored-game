using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Strategy.InteractiveObjects;

namespace SwordAndBored.Strategy.GameResources
{
    public class TestGoldDisplay : MonoBehaviour
    {
        public Gold gold;
        public City city;
        int count = 0;

        void Update()
        {
            count++;
            if (count > 16)
            {
                gold.Amount += 1;
                Debug.Log("Gold: " + gold.Amount);
                count = 0;
                city.addGold();
            }
        }
    }
}
