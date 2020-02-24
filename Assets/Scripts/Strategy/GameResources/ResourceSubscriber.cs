using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.GameResources
{
    public class ResourceSubscriber : MonoBehaviour, IResourceSubscriber
    {
        public Resource Resource { get; set; }
        public int Amount { get; set; }
        public ResourceDisplay resourceDisplay;

        void UpdateAmount()
        {
            Amount = Resource.Amount;
            resourceDisplay.UpdateDisplay();
        }
    }
}
