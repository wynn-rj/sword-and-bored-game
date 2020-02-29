using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Battlefield;
using SwordAndBored.Battlefield.CreaturScripts;

public class PlayerAbilityStateBehavior : StateMachineBehaviour
{

    BrainManager brain;
    LayerMask lm;
    int abilityToUse = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        brain = animator.GetComponent<BrainManager>();
        brain.outline.enabled = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        abilityToUse = animator.GetInteger("Ability");
        if (brain.creature.abilityContainer.IsAoe(abilityToUse))
        {
            lm = brain.selectingGroundLayerMask;
        } else
        {
            brain.creature.abilityContainer.StopAoe();
            lm = brain.selectingCreaturesLayerMask;
        }
        Ray ray = brain.cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, lm))
        {

            brain.creature.abilityContainer.HighlightTarget(abilityToUse, hit);
            if (Input.GetButtonDown("Fire1"))
            {
                brain.creature.abilityContainer.UseAbility(abilityToUse, hit);
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
        brain.creature.abilityContainer.StopAoe();
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
