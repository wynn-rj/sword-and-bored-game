using System;
using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.Strategy.BaseManagement.Buildings
{
    public class GenericStrongholdCell : MonoBehaviour, IStrongholdCell
    {
        [SerializeField] private Button clickableObject;

        public Button ClickableObject
        {
            get { return clickableObject; }
            set { value = clickableObject; }
        }

        public int Index { get; set; }

        public virtual void OnClick()
        {
            /*
             * TO DO: implement
             */
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
