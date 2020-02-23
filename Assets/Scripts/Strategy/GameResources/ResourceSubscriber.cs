using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.GameResources
{
    public class ResourceSubscriber : MonoBehaviour, IResourceSubscriber
    {
        public Gold resource;
        public int Amount { get; set; }
        public ResourceDisplay resourceDisplay;

        void Start()
        {
            if(this is null)
            {
                return;
            }
            resource.AddSubscriber(this);
            Debug.Log("Added this to RS List");
        }

        public void UpdateAmount()
        {
            Amount = resource.Amount;
            resourceDisplay.UpdateDisplay(Amount);
        }
    }
}
