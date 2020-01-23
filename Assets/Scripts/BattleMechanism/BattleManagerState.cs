
namespace SwordAndBored.BattleMechanism
{
    public abstract class BattleManagerState
    {
        protected readonly BattleManager battleManager;

        public BattleManagerState(BattleManager battleManager)
        {
            this.battleManager = battleManager;
        }

    }
}