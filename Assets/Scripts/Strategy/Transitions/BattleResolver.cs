using UnityEngine;
using SwordAndBored.SceneManagement;
using SwordAndBored.GameData.Units;

namespace SwordAndBored.Strategy.Transitions
{
    class BattleResolver : MonoBehaviour
    {
        private void Awake()
        {
            if (SceneSharing.squadID == -1)
            {
                return;
            }

            ISquad squad = new Squad(SceneSharing.squadID);
            if (GameScenes.battleWin)
            {
                foreach (EnemySquad enemy in EnemySquad.GetAllEnemySquads())
                {
                    if (enemy.X == squad.X && enemy.Y == squad.Y)
                    {
                        enemy.Delete();
                        break;
                    }
                }
            }
            else
            {
                squad.Delete();
            }

            SceneSharing.squadID = -1;
        }
    }
}
