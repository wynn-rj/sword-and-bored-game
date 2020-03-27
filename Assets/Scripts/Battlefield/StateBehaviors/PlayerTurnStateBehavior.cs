using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using SwordAndBored.Battlefield.CreaturScripts;
using SwordAndBored.Battlefield.MovementSystemScripts;
using UnityEngine.UI;

namespace SwordAndBored.Battlefield.StateBehaviors
{
    public class PlayerTurnStateBehavior : StateMachineBehaviour
    {
        private Canvas abilityCanvas;
        private AbilitySelector abilitySelector;
        private GameObject[] abilityButtons;
        BrainManager brain;
        MovementSystem ms;
        List<Tile> possible;
        UniqueCreature creature;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            brain = animator.GetComponent<BrainManager>();
            brain.indicatorRend.enabled = true;

            creature = animator.GetComponent<UniqueCreature>();
            brain.outline.enabled = true;
            ms = animator.GetComponent<MovementSystem>();
            ms.finishedMoving = true;

            List<Ability> abilityList = brain.GetComponent<UnitAbilitiesContainer>().abilities;

            abilityCanvas = brain.manager.hotbar;
            abilitySelector = abilityCanvas.GetComponent<AbilitySelector>();
            abilitySelector.abilityList = abilityList;
            abilitySelector.setAbilityList = true;
            abilitySelector.SelectedAbilityButton(-1);
            abilityButtons = abilitySelector.AbilityButtons;
            for (int i = 0; i < abilityButtons.Length; i++)
            {
                if (i < abilityList.Count)
                {
                    abilityButtons[i].GetComponent<Button>().interactable = true;
                    abilityButtons[i].GetComponent<AbilityButtonHighlight>().isEnabled = true;
                    abilityButtons[i].GetComponent<AbilityButtonHighlight>().ability = abilityList[i];
                }
                else
                {
                    abilityButtons[i].GetComponent<Button>().interactable = false;
                    abilityButtons[i].GetComponent<AbilityButtonHighlight>().isEnabled = false;
                }
            }
            possible = ms.GetPossible(creature.movementLeft);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (possible == null)
            {
                possible = ms.GetPossible(creature.movementLeft);
            }

            if (!ms.notMoving)
            {
                possible = ms.GetPossible(creature.movementLeft);
            } else if (possible != null)
            {
                ms.ShowPossible(possible);
            }
            brain.outline.OutlineColor = Color.blue;
            Ray ray = brain.cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, brain.tileMapLayerMask))
            {
                Tile endTile = hit.collider.GetComponent<Tile>();
                brain.tileIndictor.transform.position = endTile.GetCenterOfTile();
                if (endTile.unitOnTile == null && Input.GetButtonDown("Fire1") && possible != null)
                {
                    bool a = false;
                    foreach (Tile tile in possible)
                    {
                        if (endTile == tile)
                        {
                            a = true;
                        }
                    }

                    if (a)
                    {
                        if (EventSystem.current.IsPointerOverGameObject()) return;
                        ms.Move(endTile, true);
                    }
                }
            }
            if (brain.HasActionLeft())
            {
                if (abilitySelector.currentlySelectedNum > 0)
                {
                    if (ms.finishedMoving)
                    {
                        animator.SetInteger("Ability", abilitySelector.currentlySelectedNum - 1);
                        animator.SetBool("UseAbility", true);
                        abilitySelector.ResetAbilitySelection(-1);
                        ms.finishedMoving = false;
                    }
                }
            }

            if (Input.GetButtonDown("Next"))
            {
                brain.isMyTurn = false;
            }

        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            brain.indicatorRend.enabled = false;
            brain.outline.enabled = false;
            abilityButtons[0].GetComponent<AbilityButtonHighlight>().descriptionPanel.SetActive(false);
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