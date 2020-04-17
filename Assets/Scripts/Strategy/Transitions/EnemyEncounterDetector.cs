using UnityEngine;
using UnityEngine.SceneManagement;
using SwordAndBored.SceneManagement;
using SwordAndBored.Strategy.Squads;
using SwordAndBored.Strategy.EnemyManagement;
using SwordAndBored.Strategy.Movement;

namespace SwordAndBored.Strategy.Transitions
{
    public class EnemyEncounterDetector : MonoBehaviour
    {
        internal SquadManager squadManager;
        internal EnemyBrain enemyBrain;
        private bool alreadyLoading = false;

        void OnTriggerEnter(Collider other)
        {
            SquadController squadController = other.gameObject.GetComponent<SquadController>();
            if (squadController)
            {
                if (!alreadyLoading)
                {
                    SceneSharing.squadID = squadController.SquadData.ID;
                    foreach (SquadController squad in squadManager.squads)
                    {
                        squad.ClearPath();
                    }
                    foreach (EnemyMovementController squad in enemyBrain.Enemies)
                    {
                        squad.ClearPath();
                    }
                    SceneManager.LoadSceneAsync(GameScenes.BATTLEFIELD);
                    alreadyLoading = true;
                }
            }
        }
    }
}
