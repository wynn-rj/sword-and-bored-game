using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.GameResources
{
    public class GoldCity : AbstractCity
    {        
        protected override void MainThreadPostTimeStepUpdate()
        {
            if(UnderPlayerControl)
            {
                ResourceManager.GoldAmount += 1;
            }
        }
    }
}
