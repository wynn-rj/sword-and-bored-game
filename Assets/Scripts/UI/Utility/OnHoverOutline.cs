using SwordAndBored.Utilities.Debug;
using UnityEngine;

namespace SwordAndBored.UI.Utility
{
    class OnHoverOutline : MonoBehaviour
    {
        private Outline outline;

        public Color OutlineColor
        {
            get => outline.OutlineColor;
            set => outline.OutlineColor = value;
        }

        public bool OutlineEnabled
        {
            get => outline.enabled;
            set => outline.enabled = value;
        }

        private void Start()
        {
            outline = gameObject.GetComponentInChildren<Outline>();
            AssertHelper.Assert(outline != null, "An outline component must be set for " + transform.parent.name, this);
            outline.enabled = false;
        }
    }
}
