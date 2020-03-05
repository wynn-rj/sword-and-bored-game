using TMPro;
using UnityEngine;

namespace SwordAndBored.Strategy.GameResources
{
    public class ResourceDisplay : MonoBehaviour
    {
        public TMP_Text textToDisplay;

        public void UpdateDisplay(int amount)
        {
            textToDisplay.text = "Gold: " + amount;
        }
    }
}
