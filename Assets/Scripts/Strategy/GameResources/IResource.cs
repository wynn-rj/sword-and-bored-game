using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Utilities;

namespace SwordAndBored.Strategy.GameResources
{
    public interface IResource : IObserver<IResourceSubscriber>
    {
        int Amount { get; set; }

        /// <summary>
        /// Returns true if the purchase can be completed
        /// </summary>
        /// <param name="payment">The payment you want to check ability to purchase</param>
        /// <returns>Whether or not the payment can be afforded</returns>
        bool CanAffordPurchase(IPayment payment);

        /// <summary>
        /// Adds the subscriber to the observer's list of objects to be notified
        /// </summary>
        /// <param name="subscriber">The object that is subscribing to the observer</param>
        void Subscribe(IResourceSubscriber subscriber);

        /// <summary>
        /// Attempts to remove the subscriber from the list of subscribed objects
        /// </summary>
        /// <param name="subscriber">The object to remove</param>
        /// <returns>Whether the object was successfully removed</returns>
        bool Unsubscribe(IResourceSubscriber subscriber);
    }
}
