using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.GameResources
{
    public class Payment : IPayment
    {
        // The resource needed for the purchase
        public IResource resourceType { get; set; }

        // The amount of resource needed to complete the purchase
        public int cost { get; set; }
    }
}
