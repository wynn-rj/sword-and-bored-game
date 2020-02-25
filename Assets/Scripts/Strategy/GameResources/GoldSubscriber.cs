using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.GameResources
{
    public class GoldSubscriber : MonoBehaviour, IResourceSubscriber
    {
        public Gold resource;
        public int Amount { get; set; }
        public ResourceDisplay resourceDisplay;

        void Start()
        {
            resource.Subscribe(this);
        }

        public void UpdateAmount()
        {
            Amount = resource.Amount;
            resourceDisplay.UpdateDisplay(Amount);
        }
    }
}
