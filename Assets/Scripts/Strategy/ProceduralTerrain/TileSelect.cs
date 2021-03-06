﻿using UnityEngine;
using SwordAndBored.Utilities.Debug;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using SwordAndBored.Utilities.UnityHelper;
using SwordAndBored.Utilities;
using System.Collections.Generic;
using SwordAndBored.Strategy.TimeSystem.TimeManager;
using SwordAndBored.UI.Utility;
using SwordAndBored.Strategy.BaseManagement.Towns;

namespace SwordAndBored.Strategy.ProceduralTerrain
{
    public class TileSelect : OnClickOrHoverMonoBehaviour, IObserver<ITileSelectSubscriber>
    {
        private readonly IList<ITileSelectSubscriber> subscribers = new List<ITileSelectSubscriber>();
        [SerializeField] private AbstractTimeManager timeManager;
        [SerializeField] private TownCanvasController townCanvasController;
        private GameObject lastClicked;

        void Awake()
        {
            clickMask = LayerMask.GetMask(new string[] { "HexTile" });
            AssertHelper.IsSetInEditor(timeManager, this);
        }

        protected override void OnClick(RaycastHit hit)
        {
            if (timeManager.IsTimeStepAdvancing) return;
            GameObject selectedObject = hit.collider.gameObject.transform.parent.gameObject;
            AssertHelper.Assert(selectedObject.name.Contains("hextile"), "Clicked on unexpected gameobject: " + selectedObject.name, this);
            IHexGridCell clickedTile = selectedObject.GetComponent<MonoHexGridCell>().HexGridCell;
            if (clickedTile.HasComponent<UnselectableComponent>()) return;

            OnHoverOutline onHoverOutline = hit.collider.gameObject.GetComponent<OnHoverOutline>();
            if (onHoverOutline)
            {
                lastClicked = hit.collider.gameObject;
                onHoverOutline.OutlineColor = Color.cyan;
            }

            townCanvasController.DisplayedTown = null;
            ISelectionComponent selectionComponent = clickedTile.GetComponent<ISelectionComponent>();
            if (!(selectionComponent is null))
            {
                selectionComponent.Select();
            }
            foreach (ITileSelectSubscriber subscriber in subscribers)
            {
                subscriber.OnTileSelect(clickedTile);
            }
        }

        protected override void OnHover(RaycastHit hit)
        {
            OnHoverOutline onHoverOutline = hit.collider.gameObject.GetComponent<OnHoverOutline>();
            if (onHoverOutline)
            {
                onHoverOutline.OutlineEnabled = true;
            }
        }

        protected override void LostHover(GameObject hadHover)
        {
            OnHoverOutline onHoverOutline = hadHover.GetComponent<OnHoverOutline>();
            if (onHoverOutline)
            {
                onHoverOutline.OutlineEnabled = false;
            }
        }

        protected override void OnClickRelease()
        {
            OnHoverOutline onHoverOutline = lastClicked?.GetComponent<OnHoverOutline>();
            if (onHoverOutline)
            {
                onHoverOutline.OutlineColor = Color.white;
            }
        }


        public void Subscribe(ITileSelectSubscriber subscriber) => subscribers.Add(subscriber);

        public bool Unsubscribe(ITileSelectSubscriber subscriber) => subscribers.Remove(subscriber);
    }
}
