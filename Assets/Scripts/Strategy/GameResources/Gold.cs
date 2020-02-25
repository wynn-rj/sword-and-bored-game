using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.GameResources
{
    public class Gold : MonoBehaviour, IResource
    {
        private List<IResourceSubscriber> Subscribers { get; set; }
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

        void Awake()
        {
            Subscribers = new List<IResourceSubscriber>();
        }

        public bool CanAffordPurchase(IPayment payment)
        {
            return payment.cost <= mAmount;
        }

        public void Subscribe(IResourceSubscriber subscriber)
        {
            if (subscriber != null && Subscribers != null)
            {
                Subscribers.Add(subscriber);
            }
        }

        public bool Unsubscribe(IResourceSubscriber subscriber)
        {
            if(Subscribers.Contains(subscriber))
            {
                Subscribers.Remove(subscriber);
                return true;
            }
            return false;
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
