using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.GameResources
{
    public class TestGoldDisplay : MonoBehaviour
    {
        public Gold gold;
        int count = 0;

        void Update()
        {
            count++;
            if (count > 16)
            {
                gold.Amount = gold.Amount + 1;
                Debug.Log("Gold: " + gold.Amount);
                count = 0;
            }
        }
    }
}
