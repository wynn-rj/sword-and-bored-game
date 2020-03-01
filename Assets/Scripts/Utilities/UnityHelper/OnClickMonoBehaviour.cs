using UnityEngine;
using UnityEngine.EventSystems;

namespace SwordAndBored.Utilities.UnityHelper
{
    public abstract class OnClickMonoBehaviour : MonoBehaviour
    {
        protected int clickMask;
        protected float maxRayCastDistance;
        protected Camera clickCamera;

        private readonly int mouseButton;

        protected OnClickMonoBehaviour(int mouseButton = 0)
        {
            this.mouseButton = mouseButton;
            clickMask = Physics.DefaultRaycastLayers;
            maxRayCastDistance = Mathf.Infinity;            
        }

        private void Start()
        {
            clickCamera = Camera.main;
        }

        protected virtual void Update()
        {
            if (Input.GetMouseButtonDown(mouseButton))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                Ray ray = clickCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, maxRayCastDistance, clickMask))
                {
                    OnClick(hit);
                }
            }
        }

        protected abstract void OnClick(RaycastHit hit);         
    }
}
