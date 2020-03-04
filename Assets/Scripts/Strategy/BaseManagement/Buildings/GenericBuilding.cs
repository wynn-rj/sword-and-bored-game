using SwordAndBored.Strategy.BaseManagement.Buildings;
using UnityEngine;

namespace SwordAndBored.StrategyView.BaseManagement.Buildings
{
    public class GenericBuilding : MonoBehaviour, IBuilding
    {
        [SerializeField] private GameObject mainCanvas;

        protected GenericStrongholdCell strongholdCell;

        public GameObject MainCanvas => mainCanvas;

        public GameObject Canvas => mainCanvas;

        public void LoadCanvas()
        {
            mainCanvas.SetActive(true);
        }

        public void UnloadCanvas()
        {
            mainCanvas.SetActive(false);
        }
    }
}