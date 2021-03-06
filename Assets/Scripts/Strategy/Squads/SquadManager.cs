﻿using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Strategy.TimeSystem.TimeManager;
using SwordAndBored.Strategy.ProceduralTerrain;
using SwordAndBored.GameData.Units;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Utilities.Debug;
using SwordAndBored.Strategy.GameResources;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using SwordAndBored.Strategy.BaseManagement.Towns;

namespace SwordAndBored.Strategy.Squads
{
    public class SquadManager : MonoBehaviour, ISquadManager, ITileSelectSubscriber
    {
        [SerializeField] private SquadController squadPrefab;
        [SerializeField] private TimeTrackingTimeManager turnManager;
        [SerializeField] private TileManager tileManager;
        [SerializeField] private ResourceManager resourceManager;
        [SerializeField] private SquadCanvasController canvasController;
        [SerializeField] private TownCanvasController townCanvasController;
        [SerializeField] private KeyCode loseSquadFocusKey = KeyCode.Escape;
        [SerializeField] private float squadPlacementHeight = 0;
        [SerializeField] private int squadUpkeepCost = 1;
        [SerializeField] private SquadController selectedSquad;

        public readonly IList<SquadController> squads = new List<SquadController>();

        public SquadController SelectedSquad
        {
            get => selectedSquad;
            private set
            {
                if (selectedSquad)
                {
                    selectedSquad.GetComponent<Outline>().enabled = false;
                }
                if (value)
                {
                    value.GetComponent<Outline>().enabled = true;
                }
                selectedSquad = value;
                canvasController.UpdateDisplayedSquad(selectedSquad?.SquadData);
            }
        }

        public SquadController DeploySquad(string name, List<IUnit> units, IHexGridCell location)
        {
            ISquad squad = new Squad(name, units, location.Position.GridPoint.X, location.Position.GridPoint.Y);
            units.ForEach((unit) => unit.Town = null);
            squad.Save();
            return DeploySquad(squad);
        }

        public IUnit[] CollectSquad(IHexGridCell location)
        {
            SquadController squad = GetSquadOnTile(location);
            if (squad)
            {
                IUnit[] units = squad.SquadData.Units.ToArray();
                squad.SquadData.Delete();
                squads.Remove(squad);
                turnManager.Unsubscribe(squad);
                Destroy(squad);
                return units;
            }
            return null;
        }

        public void OnTileSelect(IHexGridCell selectedTile)
        {
            if (turnManager.IsTimeStepAdvancing) return;
            SquadController squad = GetSquadOnTile(selectedTile);
            if (squad)
            {
                SelectSquad(squad);
                return;
            }

            if (SelectedSquad)
            {
                if (selectedTile.GetComponent<TownComponent>() != null)// && SelectedSquad.GetComponent<IHexGridCell>().Neighbors)
                {
                    townCanvasController.LoadCanvas();
                    return;
                }

                SelectedSquad.GoTo(selectedTile);
                SelectedSquad = null;
            }
        }

        public void SelectSquad(SquadController squadToSelect)
        {
            SelectedSquad = squadToSelect;
        }

#if DEBUG
        void Awake()
        {
            AssertHelper.IsSetInEditor(squadPrefab, this);
            AssertHelper.IsSetInEditor(turnManager, this);
            AssertHelper.IsSetInEditor(tileManager, this);
            AssertHelper.IsSetInEditor(resourceManager, this);
        }
#endif

        private void Start()
        {
            tileManager.GetComponent<TileSelect>().Subscribe(this);
            foreach (ISquad squad in Squad.GetAllSquads())
            {
                DeploySquad(squad);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(loseSquadFocusKey))
            {
                SelectedSquad = null;
            }
        }

        private void OnDestroy()
        {
            foreach (SquadController controller in squads)
            {
                Point<int> pos = controller.Location.Position.GridPoint;
                controller.SquadData.X = pos.X;
                controller.SquadData.Y = pos.Y;
                controller.SquadData.Save();
            }
        }

        private SquadController DeploySquad(ISquad squad)
        {
            SquadController controller = Instantiate(squadPrefab, transform);
            IHexGridCell location = tileManager.HexTiling[squad.X, squad.Y];
            controller.transform.position = location.Position.CenterAsVector3(squadPlacementHeight);
            controller.SquadData = squad;
            controller.StartLocation = location;
            controller.UpkeepFunction = () => resourceManager.GoldAmount -= squadUpkeepCost;
            controller.IsUserControlledFunction = () => !turnManager.IsTimeStepAdvancing;
            squads.Add(controller);
            turnManager.Subscribe(controller);
            return controller;
        }

        private SquadController GetSquadOnTile(IHexGridCell tile)
        {
            foreach (SquadController squad in squads)
            {
                if (squad.Location == tile)
                {
                    return squad;
                }
            }
            return null;
        }
    }
}
