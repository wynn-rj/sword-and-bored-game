using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.GameResources
{
    public class ResourceManager : MonoBehaviour
    {
        public int GoldAmount
        {
            get { return gold; }
            set
            {
                gold = value;
                resourceDisplay.UpdateDisplay(gold);
            }
        }
        public ResourceDisplay resourceDisplay;
        private int gold;

        public bool CanAffordPurchase(IPayment payment)
        {
            return payment.Cost <= gold;
        }
    }
}
