using SwordAndBored.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SwordAndBored.Strategy.BaseManagement
{
    public class SceneHelper : MonoBehaviour
    {
        public void SwitchToMapView()
        {
            SceneManager.LoadSceneAsync(GameScenes.STRATEGYMAP);
        }
    }
}
