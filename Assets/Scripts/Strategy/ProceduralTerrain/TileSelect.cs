using UnityEngine;
using SwordAndBored.Utilities.Debug;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;
using UnityEngine.EventSystems;

namespace SwordAndBored.Strategy.ProceduralTerrain
{
    public class TileSelect : MonoBehaviour
    {
        public TileManager tileManager;
        public Material material;
        public IHexGridCell lastSelectedTile;
        public Vector3 tilePosition;
        private GameObject lastClicked;
        private Material lastClickedMaterial;

#if DEBUG
        void Awake()
        {
            AssertHelper.IsSetInEditor(tileManager, this);
        }
#endif

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
        }

        void OnClick()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (lastClicked)
            {
                lastClicked.GetComponent<MeshRenderer>().material = lastClickedMaterial;
                lastClicked = null;
                lastClickedMaterial = null;
            }
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Transform selectedTransform = hit.collider.gameObject.transform;
                Point<int> gridPoint = HexPoint.GetPointFromCenter(selectedTransform.position.x, selectedTransform.position.z, selectedTransform.localScale.x);
                lastSelectedTile = tileManager.hexTiling[gridPoint.X, gridPoint.Y];
                if (lastSelectedTile is null) return;

                ISelectionComponent selectionComponent = lastSelectedTile.GetComponent<ISelectionComponent>();
                if (!(selectionComponent is null))
                {
                    selectionComponent.Select();
                }

                // Highlight selected tile
                if (hit.collider.gameObject.name.Contains("Clone"))
                {
                    float tileHeight = hit.collider.gameObject.GetComponent<Renderer>().bounds.size.y;
                    tilePosition = selectedTransform.position + new Vector3(0, tileHeight, 0);
                    lastClicked = hit.collider.gameObject;
                    lastClickedMaterial = lastClicked.GetComponent<MeshRenderer>().material;
                    lastClicked.GetComponent<MeshRenderer>().material = material;
                    
                }
            }
        }
    }
}
