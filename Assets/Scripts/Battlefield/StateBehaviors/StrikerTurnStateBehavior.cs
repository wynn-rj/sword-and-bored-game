using UnityEngine;
using SwordAndBored.Battlefield.MovementSystemScripts;

namespace SwordAndBored.Battlefield.StateBehaviors
{
    public class StrikerTurnStateBehavior : StateMachineBehaviour
    {

        BrainManager brain;
        Tile target;
        MovementSystem ms;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            brain = animator.GetComponent<BrainManager>();
            ms = animator.GetComponent<MovementSystem>();

            brain.outline.enabled = true;
            float min = Vector3.Distance(animator.transform.position, brain.manager.playerUnits[0].transform.position);
            int playerToAttack = 0;
            for (int i = 0; i < brain.manager.playerUnits.Count; i++)
            {
                float temp = Vector3.Distance(animator.transform.position, brain.manager.playerUnits[i].transform.position);
                if (temp < min)
                {
                    min = temp;
                    playerToAttack = i;
                }
            }
            Tile targetedPlayer = brain.manager.playerUnits[playerToAttack].GetComponent<MovementSystem>().currentTile;
            PickTarget(targetedPlayer);

            if (target && target.unitOnTile == null)
            {
                ms.Move(target);
            }
            else
            {
                ms.finishedMoving = true;
                Debug.Log("Does not have target location");
            }
        }

        private void PickTarget(Tile targetedPlayer)
        {
            target = ms.grid[targetedPlayer.x + 1, targetedPlayer.y];
            if (target.unitOnTile)
            {
                target = ms.grid[targetedPlayer.x - 1, targetedPlayer.y];
            }
            if (target.unitOnTile)
            {
                target = ms.grid[targetedPlayer.x - 1, targetedPlayer.y];
            }
            if (target.unitOnTile)
            {
                target = ms.grid[targetedPlayer.x, targetedPlayer.y - 1];
            }
            if (target.unitOnTile)
            {
                target = ms.grid[targetedPlayer.x, targetedPlayer.y + 1];
            }
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            brain.outline.OutlineColor = Color.yellow;

            if (ms.finishedMoving)
            {
                animator.SetBool("UseAbility", true);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}