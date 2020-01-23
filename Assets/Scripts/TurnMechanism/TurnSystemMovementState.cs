using UnityEngine;
using UnityEngine.AI;

namespace SwordAndBored.TurnMechanism
{
    public class TurnSystemMovementState : TurnSystemState
    {
        public TurnSystemMovementState(TurnSystem turnSystem)
        : base(turnSystem)
        {
            turnSystem.EventHandler.MoveCallbackEvent += this.MoveEvent;
        }

        private void MoveEvent(Vector3 moveLocation)
        {
            NavMeshAgent agent = turnSystem.activeGameObject.GetComponent<NavMeshAgent>();
            agent.destination = moveLocation;
            turnSystem.EventHandler.MoveCallbackEvent -= this.MoveEvent;
            this.EndTurnStateTransition();
        }

        private void EndTurnStateTransition()
        {
            turnSystem.State = new TurnSystemEndState(turnSystem);
        }
    }
}