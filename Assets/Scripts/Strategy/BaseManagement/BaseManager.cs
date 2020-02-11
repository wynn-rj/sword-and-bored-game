using SwordAndBored.StrategyView.BaseManagement.Buildings;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SwordAndBored.StrategyView.BaseManagement
{
    public class BaseManager : MonoBehaviour
    {
        public IBaseManagementState BaseManagementState { get; set; }

        public Canvas ActiveCanvas { get; set; }

        public int BuildingIndex { get; set; }

        public int ActiveTier { get; set; }

        public int OverallTier { get; set; }

        public BaseGrid BaseGrid;

        //The first element is the tiers option canvas
        [SerializeField] private List<GameObject> buildingTierCanvases;
        [SerializeField] private List<UnityEngine.UI.Button> tierButtonsList;

        private IDictionary<int, Dictionary<int, Func<IBuilding>>> buildingDict = new Dictionary<int, Dictionary<int, Func<IBuilding>>>()
        {
            //Tier I buildings
            {1, new Dictionary<int, Func<IBuilding>>()
                {
                    {0, BuildingFactory.CreateBarracks },
                    {1, BuildingFactory.CreateGranary }
                }
            },

            //Tier II buildings
            {2, new Dictionary<int, Func<IBuilding>>()
                {
                    //Placeholders
                    {0, BuildingFactory.CreateBarracks },
                    {1, BuildingFactory.CreateGranary }
                }
            },

            //Tier III buildings
            {3, new Dictionary<int, Func<IBuilding>>()
                {
                    //Placeholders
                    {0, BuildingFactory.CreateBarracks },
                    {1, BuildingFactory.CreateGranary }
                }
            }
        };

        void Awake()
        {
            BaseGrid = FindObjectOfType<BaseGrid>();

            ActiveCanvas = buildingTierCanvases[0].GetComponent<Canvas>();

            ActiveTier = OverallTier = 1;
            BuildingIndex = 0;

            BaseManagementState = new IdleBaseState(this);
        }

        void Start() { }

        void Update()
        {
            BaseManagementState.Update();
        }

        public void ToggleActiveCanvas()
        {
            ActiveCanvas.gameObject.SetActive(!ActiveCanvas.gameObject.activeSelf);
        }

        public void SetActiveCanvas(int index)
        {
            ActiveCanvas = buildingTierCanvases[index].GetComponent<Canvas>();
        }

        /// <summary>
        /// Activates proper tier list canvas of buildings and ensures all others are deactivated
        /// </summary>
        /// <param name="index"></param>
        public void SetAndToggleActiveCanvas(int index)
        {
            if (index <= OverallTier)
            {
                ToggleActiveCanvas();
                SetActiveCanvas(index);
                ToggleActiveCanvas();
            }
        }

        public void SelectBuildingTier(int index)
        {
            ActiveTier = index;
            BaseManagementState.SelectBuildingTier();
        }

        public void SelectBilding(int index)
        {
            BuildingIndex = index;
            BaseManagementState.SelectBuilding();
        }

        public IBuilding GetBuilding(int tier)
        {
            return buildingDict[tier][BuildingIndex]();
        }

        public void UnlockTier(int tier)
        {
            OverallTier = tier;
            tierButtonsList[tier - 1].interactable = true;
        }

        public void SetAllCanvasInactive()
        {
            foreach (GameObject canvasObject in buildingTierCanvases)
            {
                canvasObject.SetActive(false);
            }
        }

        public void ExitCanvas()
        {
            BaseManagementState.Exit();
        }
    }
}
