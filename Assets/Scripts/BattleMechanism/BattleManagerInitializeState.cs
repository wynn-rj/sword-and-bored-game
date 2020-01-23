using UnityEngine;
using SwordAndBored.TurnMechanism;

namespace SwordAndBored.BattleMechanism
{
    public class BattleManagerInitializeState : BattleManagerState
    {

        public BattleManagerInitializeState(BattleManager battleManager, GameObject[] entities)
        : base(battleManager)
        {
            base.battleManager.TurnOrderController = new TurnOrderController(entities, new RandomShuffler<GameObject>());
            base.battleManager.ActivePlayer = battleManager.TurnOrderController.NextEntity();
            base.battleManager.State = new BattleManagerTurnSystemState(base.battleManager);
        }
    }
}