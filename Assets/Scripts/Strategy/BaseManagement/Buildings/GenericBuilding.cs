using SwordAndBored.Strategy.BaseManagement.Buildings;
using UnityEngine;

namespace SwordAndBored.StrategyView.BaseManagement.Buildings
{
    public class GenericBuilding : MonoBehaviour, IBuilding
    {
        public GenericStrongholdCell Cell;

        [SerializeField] private GameObject mainCanvas;

        public GameObject MainCanvas
        {
            get { return mainCanvas; }
        }

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