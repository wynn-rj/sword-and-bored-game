using System;
using UnityEngine;
using UnityEngine.UI;

namespace SwordAndBored.UI.BaseGUI
{
    public interface IClickable
    {
        Button ClickableObject { get; set; }

        void OnClick();

        void AddListener(Action action);

        void AddListener(Action<int> action, int index);

        void AddListener(Action<GameObject> func, GameObject gameObject);
    }
}
