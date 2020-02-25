using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.GameResources
{
    public interface IPayment
    {
        IResource Resource { get; set; }
        int Cost { get; set; }
    }

}