using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.StrategyView.GameResources
{
    public class Gold : IResource
    {
        //The amount of gold currently available to the player
        public int amount { get; set; }
        
        public Gold(int amount)
        {
            this.amount = amount;
        }

        /**
         * Returns true if the player has enough gold to afford the purchase 
         */
        public bool CanAffordPurchase(IPayment payment)
        {
            if (payment.cost <= amount)
            {
                return true;
            }
            return false;
        }

        /**
         * Completes the purchase by subtracting the amount of gold neccessary
         */
        public void Purchase(IPayment payment)
        {
            this.amount -= payment.cost;
        }

        public void addResource(int amountToAdd)
        {
            this.amount += amountToAdd;
        }
    }
}
