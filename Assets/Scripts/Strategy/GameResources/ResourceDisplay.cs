using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.GameResources
{
    public class ResourceDisplay : MonoBehaviour
    {
        public ResourceSubscriber resourceSubscriber;
        public Text text;

        void UpdateDisplay()
        {
            text.text = "Gold: " + ResourceSubscriber.Amount();
        }
    }
}
