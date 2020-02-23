using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.GameResources
{
    public interface IResource
    {
        List<IResourceSubscriber> Subscribers { get; set; }
        //The amount of resource you have
        int Amount { get; set; }

        /**
         * returns true if the Purchase can be afforded
         */
        bool CanAffordPurchase(IPayment payment);
    }
}
