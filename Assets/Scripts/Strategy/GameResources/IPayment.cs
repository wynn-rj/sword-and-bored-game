using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SwordAndBored.StrategyView.GameResources
{
    public interface IPayment
    {
        // The resource needed for the purchase
        IResource resourceType { get; set; }

        // The amount of resource needed to complete the purchase
        int cost { get; set; }
    }

}