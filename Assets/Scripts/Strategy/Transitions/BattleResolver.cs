using UnityEngine;
using SwordAndBored.SceneManagement;
using SwordAndBored.GameData.Units;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace SwordAndBored.Strategy.Transitions
{
    class BattleResolver : MonoBehaviour
    {
        private void Awake()
        {
            ResolveSquadCombat();
            ResolveTownCombat();
        }

        private void ResolveSquadCombat()
        {
            if (SceneSharing.squadID == -1)
            {
                return;
            }

            ISquad squad = new Squad(SceneSharing.squadID);
            if (GameScenes.battleWin)
            {

                List<ITown> towns = Town.GetAllTowns();

                foreach(Town town in towns)
                {
                    if(squad.X == town.X && squad.Y == town.Y)
                    {
                        town.PlayerOwned = true;
                    }
                }

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

        private void ResolveTownCombat()
        {
            int enemyTownCount = 0;
            int playerTownCount = 0;

            foreach (Town town in Town.GetAllTowns())
            {
                if (town.PlayerOwned)
                {
                    playerTownCount++;
                }
                else
                {
                    enemyTownCount++;
                }
            }

            if (playerTownCount == 0 || enemyTownCount == 0)
            {
                SceneSharing.playerWonGame = enemyTownCount == 0;
                SceneManager.LoadScene(GameScenes.ENDGAME);
            }
        }
    }
}
