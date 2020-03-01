using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.GameResources
{
    public interface IPayment
    {
        String Resource { get; set; }
        int Cost { get; set; }
    }

}