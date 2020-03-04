using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Strategy.Transitions;

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

        private void Start()
        {
            GoldAmount = SceneSharing.gold;
        }

        public bool CanAffordPurchase(IPayment payment)
        {
            return payment.Cost <= GoldAmount;
        }

        private void OnDestroy()
        {
            SceneSharing.gold = GoldAmount;
        }
    }
}
