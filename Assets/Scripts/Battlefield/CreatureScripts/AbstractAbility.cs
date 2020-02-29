using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Battlefield.CreaturScripts { 
    public abstract class AbstractAbility
    {
        public string AttackName;

        public abstract void Initialize(GameObject obj);
        public abstract void TriggerAbility(GameObject target);
        

    }
}
