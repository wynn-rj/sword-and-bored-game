using UnityEngine;

public class OptionsMenuFunctions : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public void MainMenuButtonPressed()
    {
        mainMenuCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
}
