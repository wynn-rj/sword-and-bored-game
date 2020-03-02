using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace SwordAndBored.UI.BaseGUI
{
    public interface IClickable
    {
        Button ClickableObject { get; set; }

        void OnClick();

        void AddListener(Action action);

        void AddListener(Action<int> action, int index);

        void AddListener(Action<GameObject> func, GameObject arg);
    }
}
