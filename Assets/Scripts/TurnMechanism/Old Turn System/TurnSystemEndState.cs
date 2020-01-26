using UnityEngine;
using UnityEngine.AI;

namespace SwordAndBored.TurnMechanism
{
    public class TurnSystemEndState : TurnSystemState
    {
        public TurnSystemEndState(TurnSystem turnSystem)
        : base(turnSystem)
        {
            turnSystem.EndStateMachine();
        }
    }
}