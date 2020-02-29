using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Battlefield.CreaturScripts { 
    public abstract class AbstractAbility
    {
        public string name;

        public abstract void Initialize(GameObject obj);
        public abstract void TriggerAbility(RaycastHit hit);
        

    }
}
