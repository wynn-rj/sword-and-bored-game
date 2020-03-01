using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SwordAndBored.Battlefield.CreaturScripts;
using TMPro;

namespace SwordAndBored.UI.Battlefield
{
    public class HealthBar : MonoBehaviour
    {

        UnitBase unit;
        public Slider healthBar;
        float healthRatio;
        public Text popup;
    
        void Start()
        {
            unit = GetComponentInParent<UnitBase>();
            popup.color = Color.clear;
        }

        void Update()
        {
            healthRatio = (float) unit.health / (float) unit.maxHealth;
            healthBar.value = healthRatio;

            if (popup.color.a > 0)
            {
                Color newColor = popup.color;
                newColor.a -= 1;
                popup.color = newColor;
            }
        }
    }

}
