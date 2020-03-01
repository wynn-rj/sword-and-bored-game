using UnityEngine;
using SwordAndBored.Utilities.Debug;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using SwordAndBored.Utilities.UnityHelper;
using SwordAndBored.Utilities;
using System.Collections.Generic;

namespace SwordAndBored.Strategy.ProceduralTerrain
{
    public class TileSelect : OnClickMonoBehaviour, IObserver<ITileSelectSubscriber>
    {
        [SerializeField] private Material material;
        private GameObject lastClicked;
        private Material lastClickedMaterial;
        private readonly IList<ITileSelectSubscriber> subscribers = new List<ITileSelectSubscriber>();

        void Awake()
        {
            AssertHelper.IsSetInEditor(material, this);
            clickMask = LayerMask.GetMask(new string[] { "HexTile" });
        }

        protected override void OnClick(RaycastHit hit)
        {
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

            // Highlight selected tile
            if (lastClicked)
            {
                lastClicked.GetComponent<MeshRenderer>().material = lastClickedMaterial;
                lastClicked = null;
                lastClickedMaterial = null;
            }
            lastClicked = hit.collider.gameObject;
            lastClickedMaterial = lastClicked.GetComponent<MeshRenderer>().material;
            lastClicked.GetComponent<MeshRenderer>().material = material; 
        }

        public void Subscribe(ITileSelectSubscriber subscriber) => subscribers.Add(subscriber);

        public bool Unsubscribe(ITileSelectSubscriber subscriber) => subscribers.Remove(subscriber);
    }
}
