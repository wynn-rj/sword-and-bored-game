using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenuFunctions : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public void MainMenuButtonPressed()
    {
        mainMenuCanvas.SetActive(true);
        gameObject.SetActive(false);        
    }
}
