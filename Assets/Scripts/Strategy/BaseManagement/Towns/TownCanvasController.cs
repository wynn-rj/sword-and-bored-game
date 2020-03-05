using SwordAndBored.GameData.Units;
using UnityEngine;

namespace SwordAndBored.Strategy.BaseManagement.Towns
{
    class TownCanvasController :MonoBehaviour
    {
        private Canvas canvas;
        private ITown displayedTown;

        public ITown DisplayedTown
        {
            get => displayedTown;
            set
            {
                displayedTown = value;
                UpdateDisplay();
            }
        }

        private void Start()
        {
            canvas = GetComponent<Canvas>();
        }

        private void UpdateDisplay()
        {
            
        }
    }
}
