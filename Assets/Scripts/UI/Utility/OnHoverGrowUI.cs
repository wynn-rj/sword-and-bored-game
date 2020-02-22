using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SwordAndBored.UI.Utility
{
    public class OnHoverGrowUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Vector3 Scale;

        private Vector3 originalScale;
        private RectTransform rt;

        private void Awake()
        {
            Scale = transform.localScale + new Vector3(0.01f, 0.01f, 0.01f);
            rt = gameObject.GetComponent<RectTransform>();
            originalScale = transform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale = Scale;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = originalScale;
        }
    }
}