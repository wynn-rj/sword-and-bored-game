using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.StrategyView.GameResources
{


    public interface IResource
    {
        //The amount of resource you have
        int amount { get; set; }

        /**
         * returns true if the Purchase can be afforded
         */
        bool CanAffordPurchase(IPayment payment);

        /**
         * completes the purchase
         */ 
        void Purchase(IPayment payment);
    }
}
