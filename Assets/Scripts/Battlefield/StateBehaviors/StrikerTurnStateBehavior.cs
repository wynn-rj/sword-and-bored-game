﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Battlefield;
using SwordAndBored.Battlefield.CreaturScripts;

public class StrikerTurnStateBehavior : StateMachineBehaviour
{

    BrainManager brain;
    Tile target;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        brain = animator.GetComponent<BrainManager>();
        brain.outline.enabled = true;
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
        Tile target = brain.manager.playerUnits[playerToAttack].GetComponent<UniqueCreature>().currentTile;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        brain.outline.OutlineColor = Color.yellow;
        if (target)
        {
            brain.creature.Move(target);
        }

        if (target && Vector3.Distance(animator.transform.position, target.GetCenterOfTile()) < 2f)
        {
            brain.isMyTurn = false;
        }

        if (Input.GetButtonDown("Next"))
        {
            brain.isMyTurn = false;
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