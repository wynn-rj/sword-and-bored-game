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

        UniqueCreature unit;
        public Slider healthBar;
        public Image fillImage;
        float healthRatio;
        public Text popup;

        void Start()
        {
            unit = GetComponentInParent<UniqueCreature>();
            popup.color = Color.clear;

            if (!unit.isEnemy)
            {
                fillImage.color = Color.blue;
            }
        }

        void Update()
        {
            healthRatio = (float)unit.health / (float)unit.maxHealth;
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
