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

        public bool CanAffordPurchase(IPayment payment)
        {
            return payment.cost <= mAmount;
        }

        public void AddSubscriber(IResourceSubscriber resourceSubscriber)
        {
            if (resourceSubscriber is null || Subscribers.Contains(resourceSubscriber))
            {
                return;
            }
            Subscribers.Add(resourceSubscriber);
        }

        void UpdateSubscribers()
        {
            /*foreach (IResourceSubscriber rs in Subscribers)
            {
                rs.UpdateAmount();
            }*/
        }
    }
}
