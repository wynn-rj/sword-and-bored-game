using SwordAndBored.Strategy.BaseManagement.Buildings;
using SwordAndBored.Strategy.BaseManagement.Units;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.BaseManagement
{
    public class BaseManager : MonoBehaviour
    {
        private Canvas activeCanvas;
        private Canvas previouslyActiveCanvas;

        [SerializeField] private GameObject StrongholdCanvas;

        public BuildingsManager BuildingManager;

        private void Awake()
        {
            activeCanvas = StrongholdCanvas.GetComponent<Canvas>();
            previouslyActiveCanvas = StrongholdCanvas.GetComponent<Canvas>();

            UnitManager.Instance.GetAllData();
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
