using SwordAndBored.Strategy.BaseManagement.Buildings;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.BaseManagement
{
    public class BaseManager : MonoBehaviour
    {
        public GameObject StrongholdCanvas;

        private IList<IStrongholdCell> cells;

        public BuildingsManager BuildingManager;
        public UnitManager UnitManager;

        private Canvas activeCanvas;
        private Canvas previouslyActiveCanvas;
  
        void Awake()
        {
            activeCanvas = StrongholdCanvas.GetComponent<Canvas>();
            previouslyActiveCanvas = StrongholdCanvas.GetComponent<Canvas>();    
        }

        /// <summary>
        /// Called on button click of active stronghold cells to switch to their canvas.
        /// </summary>
        /// <param name="newCanvas"></param>
        public void SwitchActiveCanvas(GameObject newCanvas)
        {
            activeCanvas.gameObject.SetActive(false);
            previouslyActiveCanvas = activeCanvas;
            activeCanvas = newCanvas.GetComponent<Canvas>();
            activeCanvas.gameObject.SetActive(true);
        }

        public void LoadPreviousActiveCanvas()
        {
            activeCanvas.gameObject.SetActive(false);
            Canvas temp = previouslyActiveCanvas;
            previouslyActiveCanvas = activeCanvas;
            activeCanvas = temp;
            activeCanvas.gameObject.SetActive(true);
        }

        public void UnloadStrongholdCanvas()
        {
            StrongholdCanvas.SetActive(false);
        }
    }
}
