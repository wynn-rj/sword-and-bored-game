using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwordAndBored.Battlefield;
using UnityEngine.EventSystems;
using SwordAndBored.Battlefield.AstarStuff;
using SwordAndBored.Battlefield.CreaturScripts;
using SwordAndBored.Battlefield.MovementSystemScripts;
using UnityEngine.UI;

public class PlayerTurnStateBehavior : StateMachineBehaviour
{
    private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
    };
    private Canvas abilityCanvas;
    private AbilitySelector abilitySelector;
    private GameObject[] abilityButtons;
    BrainManager brain;
    MovementSystem ms;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        brain = animator.GetComponent<BrainManager>();
        brain.indicatorRend.enabled = true;
        brain.outline.enabled = true;
        ms = animator.GetComponent<MovementSystem>();

        List<Ability> abilityList = brain.GetComponent<UnitAbilitiesContainer>().abilities;
/*        foreach(Ability inList in abilityList)
        {
            Debug.Log($"Name: {inList.name}, Damage: {inList.damage}, Accuracy: {inList.accuraccy}, AOE: {inList.aoe}, Range: {inList.range}");
        }*/

        abilityCanvas = brain.manager.hotbar;
        abilitySelector = abilityCanvas.GetComponent<AbilitySelector>();
        abilitySelector.SelectedAbilityButton(-1);
        abilityButtons = abilitySelector.AbilityButtons;
        for(int i=0; i<abilityButtons.Length; i++)
        {
            if(i<abilityList.Count)
            {
                abilityButtons[i].GetComponent<Button>().interactable = true;
            } else
            {
                abilityButtons[i].GetComponent<Button>().interactable = false;
            }
        }

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        brain.outline.OutlineColor = Color.blue;
        Ray ray = brain.cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, brain.tileMapLayerMask))
        {
            Tile endTile = hit.collider.GetComponent<Tile>();
            brain.tileIndictor.transform.position = endTile.GetCenterOfTile();
            if (endTile.unitOnTile == null && Input.GetButtonDown("Fire1"))
            {

                if (EventSystem.current.IsPointerOverGameObject()) return;
                ms.Move(endTile, true);
            }
        }

        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                animator.SetInteger("Ability", i);
                animator.SetBool("UseAbility", true);
            } else if(abilitySelector.currentlySelectedNum > 0)
            {
                animator.SetInteger("Ability", abilitySelector.currentlySelectedNum -1);
                animator.SetBool("UseAbility", true);
                abilitySelector.ResetAbilitySelection(-1);
            }
        }

        if (Input.GetButtonDown("Next")) {
            brain.isMyTurn = false;
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        brain.indicatorRend.enabled = false;
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
