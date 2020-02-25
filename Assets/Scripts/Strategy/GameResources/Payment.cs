﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.GameResources
{
    public class Payment : IPayment
    {
        public IResource Resource { get; set; }
        public int Cost { get; set; }
    }
}
