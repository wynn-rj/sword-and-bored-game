using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.StrategyView.GameResources
{


    public interface IResource
    {
        int amount { get; set; }
        bool CanAffordPurchase(IPayment payment);
        void Purchase(IPayment payment);
    }
}
