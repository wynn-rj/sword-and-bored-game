using UnityEngine;
using System.Collections.Generic;
using SwordAndBored.Battlefield.MovementSystemScripts;
using SwordAndBored.Battlefield.CreaturScripts;

namespace SwordAndBored.Battlefield.StateBehaviors
{
    public class StrikerAbilityTurnBehavior : StateMachineBehaviour
    {
        BrainManager brain;
        GameObject Target;
        UnitAbilitiesContainer abilitiesContainer;
        MovementSystem ms;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            brain = animator.GetComponent<BrainManager>();
            ms = animator.GetComponent<MovementSystem>();

            float min = Vector3.Distance(animator.transform.position, brain.manager.playerUnits[0].transform.position);
            int playerToAttack = 0;
            for (int i = 1; i < brain.manager.playerUnits.Count; i++)
            {
                float temp = Vector3.Distance(animator.transform.position, brain.manager.playerUnits[i].transform.position);
                if (temp < min)
                {
                    min = temp;
                    playerToAttack = i;
                }
            }
            Target = brain.manager.playerUnits[playerToAttack];
            abilitiesContainer = brain.creature.abilityContainer;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Ability selectedAbility = null;
            float distance = Vector3.Distance(animator.gameObject.transform.position, Target.transform.position);
            for (int i=0; i<abilitiesContainer.abilities.Count; i++)
            {
                Ability ability = abilitiesContainer.abilities[i];
                if (ability.range >= distance - .1f)
                {
                    selectedAbility = ability;
                    if (ability.damage > selectedAbility.damage)
                    {
                        selectedAbility = ability;
                    }
                }
            }
            if (selectedAbility is null)
            {
                Debug.Log("No Valid Ability");
            } else
            {
                selectedAbility.EnemyAttackNonAOE(Target.GetComponent<UniqueCreature>());
            }
            animator.SetBool("UseAbility", false);
            ms.finishedMoving = false;
            brain.isMyTurn = false;
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