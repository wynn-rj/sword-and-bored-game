using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.Strategy.BaseManagement.Buildings
{
    public class BuildingsManager : MonoBehaviour
    {
        private IDictionary<int, GameObject> buildingsDict = new Dictionary<int, GameObject>();
        private int ActiveCellIndex;

        public BaseManager baseManager;
        public Canvas buildingsCanvas;      
        public GameObject Barracks;

        void Awake()
        {
            LoadBuildingsDicionary();
        }

        public void TakeActionOnCell(int index)
        {
            ActiveCellIndex = index;

            GameObject cellObject = gameObject.transform.parent.GetChild(index).gameObject;
            cellObject.GetComponent<IStrongholdCell>().OnClick();
        }

        public void Build(int index)
        {
            GameObject cellObject = gameObject.transform.parent.GetChild(ActiveCellIndex).gameObject;
            GameObject newCellObject = Instantiate(buildingsDict[index], Vector3.zero, Quaternion.identity);

            //Commented code will be used.

            /*IStrongholdCell cellComponent = newCellObject.GetComponent<IStrongholdCell>();
            Action<GameObject> action = a => baseManager.UnloadActiveCanvas(cellComponent.Canvas);
            cellComponent.AddListener(action, cellComponent.Canvas);*/

            newCellObject.transform.SetParent(gameObject.transform.parent);
            newCellObject.transform.SetSiblingIndex(ActiveCellIndex);
            Destroy(cellObject);
            //baseManager.Cells[ActiveCellIndex] = newCellObject;   
            ToggleBuildingsCanvas();
        }

        public void ToggleBuildingsCanvas()
        { 
            buildingsCanvas.gameObject.SetActive(!buildingsCanvas.gameObject.activeSelf);
        }

        private void LoadBuildingsDicionary()
        {
            buildingsDict[0] = Barracks;
        }
    }
}
