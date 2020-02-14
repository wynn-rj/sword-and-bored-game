using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SwordAndBored.StrategyView.GameResources
{
    public interface IPayment
    {
        IResource resourceType { get; set; }
        int cost { get; set; }
    }

}