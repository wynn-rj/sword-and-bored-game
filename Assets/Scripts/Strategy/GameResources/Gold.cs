using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.GameResources
{
    public class Gold : MonoBehaviour, IResource
    {
        public List<IResourceSubscriber> Subscribers { get; set; }
        //The amount of gold currently available to the player
        public int Amount 
        { 
            get { return mAmount; }
            set
            {
                mAmount = value;
                UpdateSubscribers();
            }
        }
        int mAmount;

        /**
         * Returns true if the player has enough gold to afford the purchase 
         */
        public bool CanAffordPurchase(IPayment payment)
        {
            return payment.cost <= mAmount;
        }

        public void AddSubscriber(IResourceSubscriber resourceSubscriber)
        {
            if(resourceSubscriber != null)
            {
                Subscribers.Add(resourceSubscriber);
            }
        }

        void UpdateSubscribers()
        {
            foreach (IResourceSubscriber rs in Subscribers)
            {
                rs.UpdateAmount();
            }
        }
    }
}
