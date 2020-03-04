using UnityEngine;
using UnityEngine.EventSystems;

namespace SwordAndBored.Utilities.UnityHelper
{
    public abstract class OnClickOrHoverMonoBehaviour : OnClickMonoBehaviour
    {
        private GameObject hasHover;

        protected OnClickOrHoverMonoBehaviour(int mouseButton = 0) : base(mouseButton) { }

        protected override void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            Ray ray = clickCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, maxRayCastDistance, clickMask))
            {
                if (hit.collider.gameObject != hasHover)
                {
                    if (hasHover)
                    {
                        LostHover(hasHover);
                    }
                    OnHover(hit);
                    hasHover = hit.collider.gameObject;
                }
                if (Input.GetMouseButtonDown(mouseButton))
                {
                    OnClick(hit);
                }
            }
            else
            {
                if (hasHover)
                {
                    LostHover(hasHover);
                    hasHover = null;
                }
            }
            if (Input.GetMouseButtonUp(mouseButton))
            {
                OnClickRelease();
            }
        }

        protected abstract void OnHover(RaycastHit hit);

        protected virtual void LostHover(GameObject hadHover) { }
    }
}
