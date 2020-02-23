using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.GameResources
{
    public interface IResourceSubscriber
    {
        Resource Resource { get; set; }
        int Amount { get; set; }
    }
}