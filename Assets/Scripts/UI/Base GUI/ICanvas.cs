using UnityEngine;

namespace SwordAndBored.UI.BaseGUI
{
    public interface ICanvas
    {
        GameObject Canvas { get; set; }

        void LoadCanvas();

        void UnloadCanvas();
    }
}
