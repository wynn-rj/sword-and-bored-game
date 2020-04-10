using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Battlefield.CreaturScripts { 
    public abstract class AbstractAbility
    {
        public string name;

        public abstract void ShowTarget(RaycastHit hit);
        public abstract void Initialize(UnitAbilitiesContainer container, GameObject obj, GameObject shape);
        public abstract bool TriggerAbility(RaycastHit hit);
        

    }
}
