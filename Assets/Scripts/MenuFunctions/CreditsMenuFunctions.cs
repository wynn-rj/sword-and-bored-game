using UnityEngine;

namespace SwordAndBored.UI.MenuFunctions
{
    public class CreditsMenuFunctions : MonoBehaviour
    {
        public GameObject mainMenuCanvas;
        public void MainMenuButtonPressed()
        {
            mainMenuCanvas.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}