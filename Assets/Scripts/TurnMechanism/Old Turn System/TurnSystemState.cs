
namespace SwordAndBored.TurnMechanism
{
    public abstract class TurnSystemState
    {
        protected TurnSystem turnSystem;

        public TurnSystemState(TurnSystem turnSystem)
        {
            this.turnSystem = turnSystem;
        }
    }
}