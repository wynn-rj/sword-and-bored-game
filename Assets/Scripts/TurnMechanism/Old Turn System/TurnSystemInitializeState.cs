using UnityEngine;

namespace SwordAndBored.TurnMechanism
{
    public class TurnSystemInitializeState : TurnSystemState
    {
        public TurnSystemInitializeState(TurnSystem turnSystem)
        : base(turnSystem)
        {
            turnSystem.State = new TurnSystemMovementState(base.turnSystem);
        }
    }
}