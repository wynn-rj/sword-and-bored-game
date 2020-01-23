using SwordAndBored.TurnMechanism;

namespace SwordAndBored.BattleMechanism
{
    public class BattleManagerTurnSystemState : BattleManagerState
    {
        public BattleManagerTurnSystemState(BattleManager battleManager)
        : base(battleManager)
        {
            TurnSystem.Instance.SetupSystem(battleManager.ActivePlayer, this.TurnSystemEndTransition);
            TurnSystem.Instance.RunSystem();
        }

        private void TurnSystemEndTransition()
        {
            TurnSystem.Instance.CleanUpSystem(this.TurnSystemEndTransition);
            battleManager.State = new BattleManagerChangeTurnState(battleManager);
        }
    }


}