using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SwordAndBored.Battlefield.CreaturScripts;

namespace SwordAndBored.UI.Battlefield
{
    public class HealthBar : MonoBehaviour
    {

        CreatureBase unit;
        public Slider healthBar;
        float healthRatio;
    
        void Start()
        {
            unit = GetComponentInParent<CreatureBase>();
        }

    
        void Update()
        {
            healthRatio = (float) unit.health / (float) unit.maxHealth;
            healthBar.value = healthRatio;
        }
    }

}
