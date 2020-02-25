using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.GameResources
{
    public interface IPayment
    {
        // The resource needed for the purchase
        IResource ResourceType { get; set; }

        // The amount of resource needed to complete the purchase
        int Cost { get; set; }
    }

}