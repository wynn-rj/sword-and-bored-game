using UnityEngine;
using SwordAndBored.Strategy.Squads;
using SwordAndBored.Strategy.EnemyManagement;

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
                    BattleStarter.StartBattle(squadController);
                    alreadyLoading = true;
                }
            }
        }
    }
}
