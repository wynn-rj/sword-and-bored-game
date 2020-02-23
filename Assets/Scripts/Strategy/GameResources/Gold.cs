using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.GameResources
{
    public class Gold : MonoBehaviour, IResource
    {
        List<IResourceSubscribers> Subscribers { get; set; }
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

        public bool CanAffordPurchase(IPayment payment)
        {
            return payment.cost <= amount;
        }

        public void AddSubscriber()
        {

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
