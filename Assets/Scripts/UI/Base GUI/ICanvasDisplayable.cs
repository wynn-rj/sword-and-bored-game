using UnityEngine;

namespace SwordAndBored.UI.BaseGUI
{
    public interface ICanvasDisplayable
    {
        GameObject Canvas { get; }

        void LoadCanvas();

        void UnloadCanvas();
    }
}
