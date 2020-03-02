using UnityEngine;

namespace SwordAndBored.UI.BaseGUI
{
    public interface ICanvasDisplayable
    {
        GameObject Canvas { get; set; }

        void LoadCanvas();

        void UnloadCanvas();
    }
}
