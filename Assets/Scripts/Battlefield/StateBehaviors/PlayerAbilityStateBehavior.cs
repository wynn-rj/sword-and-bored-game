using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Battlefield;
using SwordAndBored.Battlefield.CreaturScripts;

public class PlayerAbilityStateBehavior : StateMachineBehaviour
{

    BrainManager brain;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        brain = animator.GetComponent<BrainManager>();
        brain.outline.enabled = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Ray ray = brain.cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, brain.selectingCreaturesLayerMask))
        {
            
            GameObject target = hit.collider.gameObject;
            UniqueCreature enem = target.GetComponent<UniqueCreature>();
            if (enem && enem != brain.creature)
            {
                enem.hightlight();
            }
            if (Input.GetButtonDown("Fire1"))
            {
                brain.creature.abilityContainer.UseAbility(0, hit);

                animator.SetBool("UseAbility", false);

            }

        }

        if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Fire2"))
        {
            animator.SetBool("UseAbility", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        brain.outline.enabled = false;
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
