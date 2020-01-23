
namespace SwordAndBored.BattleMechanism
{
    public class BattleManagerChangeTurnState : BattleManagerState
    {
        public BattleManagerChangeTurnState(BattleManager battleManager)
        : base(battleManager)
        {
            base.battleManager.ActivePlayer = battleManager.TurnOrderController.NextEntity();
            base.battleManager.State = new BattleManagerTurnSystemState(base.battleManager);
        }

    }
}