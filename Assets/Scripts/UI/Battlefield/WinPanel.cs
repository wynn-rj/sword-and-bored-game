using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SwordAndBored.SceneManagement;

public class WinPanel : MonoBehaviour
{
    public TMP_Text lootText;
    public Button worldButton;
    public Button chestButton;

    public void ClickOnChest()
    {
        Debug.Log("Roll Inventory");
        lootText.gameObject.SetActive(true);
        lootText.text = "You got a ############";
        chestButton.gameObject.SetActive(false);
        worldButton.gameObject.SetActive(true);
    }

    public void ClickWorldMap()
    {
        SceneManager.LoadSceneAsync(GameScenes.STRATEGYMAP);
    }
}
