using UnityEngine;
using UnityEngine.SceneManagement;
using SwordAndBored.SceneManagement;
using SwordAndBored.Strategy.Squads;

namespace SwordAndBored.Strategy.Transitions
{
    public class EnemyEncounterDetector : MonoBehaviour
    {
        private bool alreadyLoading = false;
        void OnTriggerEnter(Collider other)
        {
            SquadController squadController = other.gameObject.GetComponent<SquadController>();
            if (squadController)
            {
                if (!alreadyLoading)
                {
                    SceneSharing.squadID = squadController.SquadData.ID;
                    SceneManager.LoadSceneAsync(GameScenes.BATTLEFIELD);
                    alreadyLoading = true;
                }
            }
        }
    }
}
