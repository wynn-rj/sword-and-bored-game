using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.GameResources
{
    public interface IResourceSubscriber
    {
        int Amount { get; set; }
        void UpdateAmount();
    }
}