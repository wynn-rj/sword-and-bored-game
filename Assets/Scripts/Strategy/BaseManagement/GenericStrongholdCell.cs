using System;
using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.Strategy.BaseManagement
{
    public class GenericStrongholdCell : MonoBehaviour, IStrongholdCell
    {
        [SerializeField] private GameObject canvas;
        [SerializeField] private Button clickableObject;

        public GameObject Canvas
        {
            get { return canvas; }
            set { value = canvas; }
        }

        public Button ClickableObject
        {
            get { return clickableObject; }
            set { value = clickableObject; }
        }

        public int Index { get; set; }

        protected void SetCanvas(GameObject _canvas)
        {
            canvas = _canvas;
        }

        public void LoadCanvas()
        {
            canvas.SetActive(true);
        }

        public void UnloadCanvas()
        {
            canvas.SetActive(false);
        }

        public virtual void OnClick()
        {
            LoadCanvas();
        }

        public void AddListener(Action action)
        {
            clickableObject.onClick.AddListener(() => action());
        }

        public void AddListener(Action<int> action, int index)
        {
            clickableObject.onClick.AddListener(() => action(index));
        }

        public void AddListener(Action<GameObject> action, GameObject gameObject)
        {
            clickableObject.onClick.AddListener(() => action(gameObject));
        }
    }
}
