using System;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.BaseManagement
{
    public class BaseManager : MonoBehaviour
    {
        public List<GameObject> Cells;
        public BuildingsManager BuildingManager;
        public UnitManager UnitManager;
        public List<GameObject> CanvasList;

        private Canvas activeCanvas;
        private Canvas previouslyActiveCanvas;
  
        void Awake()
        {
            activeCanvas = CanvasList[0].GetComponent<Canvas>();
            previouslyActiveCanvas = CanvasList[0].GetComponent<Canvas>();    
        }

        private void Start()
        {
            LoadCells();
        }

        private void LoadCells()
        {
            // This code will be used.

            /*int index = 0;
            foreach (GameObject cell in Cells)
            {
                IStrongholdCell cellComponent = cell.GetComponent<IStrongholdCell>();
                cellComponent.AddListener(p => BuildingManager.TakeActionOnCell(index), index);
                cellComponent.Index = index;
                index++;
            }*/
        }

        public void UnloadActiveCanvas(GameObject newCanvas)
        {
            activeCanvas.gameObject.SetActive(false);
            previouslyActiveCanvas = activeCanvas;
            activeCanvas = newCanvas.GetComponent<Canvas>();
        }
    }
}
