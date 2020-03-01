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
            if (other.name.Contains("Squad"))
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
