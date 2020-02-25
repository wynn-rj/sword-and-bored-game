using UnityEngine;
using UnityEngine.SceneManagement;
using SwordAndBored.SceneManagement;

namespace SwordAndBored.Strategy.Transitions
{
    public class EnemyEncounterDetector : MonoBehaviour
    {
        private bool alreadyLoading = false;
        void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.name);
            if (other.name.Contains("Player"))
            {
                if (!alreadyLoading)
                {
                    SceneManager.LoadSceneAsync(GameScenes.BATTLEFIELD);
                    alreadyLoading = true;
                }
            }
        }
    }
}
