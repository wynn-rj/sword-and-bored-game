using UnityEngine;
using UnityEngine.SceneManagement;
using SwordAndBored.SceneManagement;

namespace SwordAndBored.Strategy.Transitions
{
    public class EnemyEncounterDetector : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.name);
            if (other.name.Contains("Player"))
            {
                SceneManager.LoadSceneAsync(GameScenes.BATTLEFIELD);
            }
        }
    }
}
