using UnityEngine;
using SwordAndBored.Utilities.Debug;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using SwordAndBored.Utilities.UnityHelper;
using SwordAndBored.Utilities;
using System.Collections.Generic;
using SwordAndBored.Strategy.TimeSystem.TimeManager;

namespace SwordAndBored.Strategy.ProceduralTerrain
{
    public class TileSelect : OnClickMonoBehaviour, IObserver<ITileSelectSubscriber>
    {
        private readonly IList<ITileSelectSubscriber> subscribers = new List<ITileSelectSubscriber>();
        [SerializeField] private AbstractTimeManager timeManager;

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

        public void Subscribe(ITileSelectSubscriber subscriber) => subscribers.Add(subscriber);

        public bool Unsubscribe(ITileSelectSubscriber subscriber) => subscribers.Remove(subscriber);
    }
}
