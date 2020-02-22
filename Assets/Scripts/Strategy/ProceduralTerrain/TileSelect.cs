using UnityEngine;
using SwordAndBored.Utilities.Debug;
using SwordAndBored.Strategy.ProceduralTerrain.Map.Grid.Cells;
using SwordAndBored.Strategy.ProceduralTerrain.Map.TileComponents;

namespace SwordAndBored.Strategy.ProceduralTerrain
{
    public class TileSelect : MonoBehaviour
    {
        public TileManager tileManager;
        public Material material;
        public Vector3 center;
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(!(lastClicked is null))
            {
                lastClicked.GetComponent<MeshRenderer>().material = lastClickedMaterial;
            }
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //if(hit.collider.gameObject.name != "Player")
                if(hit.collider.gameObject.name.Contains("Clone"))
                {
                    lastClicked = hit.collider.gameObject;
                    lastClickedMaterial = lastClicked.GetComponent<MeshRenderer>().material;
                    lastClicked.GetComponent<MeshRenderer>().material = material;
                    Transform selectedTransform = hit.collider.gameObject.transform;
                    Point<int> gridPoint = HexPoint.GetPointFromCenter(selectedTransform.position.x, selectedTransform.position.z, selectedTransform.localScale.x);
                    center = new Vector3(selectedTransform.position.x, selectedTransform.position.y + 6.5f, selectedTransform.position.z);
                    IHexGridCell tile = tileManager.hexTiling[gridPoint.X, gridPoint.Y];

                    ISelectionComponent selectionComponent = tile.GetComponent<ISelectionComponent>();
                    if (!(selectionComponent is null))
                    {
                        selectionComponent.Select();
                    }
                }
            }
            else
            {
                lastClicked = null;
                lastClickedMaterial = null;
            }
        }
    }
}
