using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SwordAndBored.SceneManagement;
namespace SwordAndBored.UI.Battlefield
{
    public class LosePanel : MonoBehaviour
    {
        public void ClickWorldMap()
        {
            SceneManager.LoadSceneAsync(GameScenes.STRATEGYMAP);
        }
    }
}