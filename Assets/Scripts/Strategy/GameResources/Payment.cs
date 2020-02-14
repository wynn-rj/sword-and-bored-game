using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.StrategyView.GameResources
{
    public class Payment : IPayment
    {
        public IResource resourceType { get; set; }
        public int cost { get; set; }
    }
}
