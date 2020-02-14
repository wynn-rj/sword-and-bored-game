using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.StrategyView.GameResources
{
    public class Gold : IResource
    {
        public int amount { get; set; }
        
        public Gold(int amount)
        {
            this.amount = amount;
        }

        public bool CanAffordPurchase(IPayment payment)
        {
            if (payment.cost <= amount)
            {
                return true;
            }
            return false;
        }

        public void Purchase(IPayment payment)
        {
            this.amount -= payment.cost;
        }
    }
}
