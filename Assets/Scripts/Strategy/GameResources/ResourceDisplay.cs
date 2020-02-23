using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.Strategy.GameResources
{
    public class ResourceDisplay : MonoBehaviour
    {
        public Text textToDisplay;

        public void UpdateDisplay(int amount)
        {
            textToDisplay.text = "Gold: " + amount;
        }
    }
}
