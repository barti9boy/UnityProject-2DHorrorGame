using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGFXScript : MonoBehaviour
{
    public PlayerStateMachine playerStateMachine;
    public PlayerStateIdle idleState;
    public PlayerStateMoving movingState;
    public PlayerStateHiding hidingState;
    public PlayerStateTryingToHide tryingToHideState;
    public PlayerStateLeavingHideout leavingHideoutState;
    public PlayerStateDead deadState;
    public PlayerStateUsingLadder usingLadderState;
    public PlayerStateUsingHorizontalDoor usingHorizontalDoorState;
    public PlayerStateUsingVerticalDoor usingVerticalDoorState;
    public PlayerStateItemPickup itemPickupState;
    public Animator animator;
    private bool isIdle;
    private bool isMoving;
    private bool isHidden;
    private bool isHiding;
    private bool isLeaving;
    private bool isClimbingDown;
    private bool isClimbingUp;
    private bool isInVent;
    private bool isEnteringVent;
    private bool isExitingVents;
    private bool isPickingUpItem;
    private bool isOpeningHorizontalDoor;
    private bool isHideout;
    private bool isCloset;
    private bool isTable;


    private void Start()
    {
        animator = GetComponent<Animator>();


        playerStateMachine = GetComponentInParent<PlayerStateMachine>();
        idleState = playerStateMachine.idleState;
        movingState = playerStateMachine.movingState;
        tryingToHideState = playerStateMachine.tryingToHideState;
        hidingState = playerStateMachine.hidingState;
        deadState = playerStateMachine.deadState;
        usingLadderState = playerStateMachine.usingLadderState;
        leavingHideoutState = playerStateMachine.leavingHideoutState;
        usingHorizontalDoorState = playerStateMachine.usingHorizontalDoorState;
        usingVerticalDoorState = playerStateMachine.usingVerticalDoorState;
        itemPickupState = playerStateMachine.itemPickupState;

        //subscribing to events
        idleState.OnEnterStateIdle += IdleState_OnEnterStateIdle;
        movingState.OnEnterStateMoving += MovingState_OnEnterStateMoving;
        tryingToHideState.OnEnterStateMoving += MovingState_OnEnterStateMoving;
        hidingState.OnEnterStateHidden += HidingState_OnEnterStateHidden;
        usingLadderState.OnStartMoving += MovingState_OnEnterStateMoving;
        usingLadderState.OnGoIntoVents += UsingLadderState_OnGoIntoVents;
        usingLadderState.OnLadderMoveDown += Climbing_OnLadderMoveDown;
        usingLadderState.OnLadderMoveUp += Climbing_OnLadderMoveDown;
        usingLadderState.OnFinishClimbing += OnFinishClimbing;
        usingLadderState.OnVentEnterOrLeave += OnVentEnterOrLeave;
        tryingToHideState.OnEnterStateHiding += HidingState_OnEnterStateHiding;
        leavingHideoutState.OnLeaveStateHiding += LeavingState_OnLeaveStateHiding;
        leavingHideoutState.OnTurnOffFurnitureTag += LeavingState_OnTurnOffFurnitureTag;
        usingHorizontalDoorState.OnStartMoving += MovingState_OnEnterStateMoving;
        usingHorizontalDoorState.OnOpeningHorizontalDoor += UsingHorizontalDoorState_OnOpeningHorizontalDoor;
        usingVerticalDoorState.OnStartMoving += MovingState_OnEnterStateMoving;
        itemPickupState.OnEnterStateItemPickup += ItemPickupState_OnEnterStateItemPickup;



    }

    private void UsingHorizontalDoorState_OnOpeningHorizontalDoor(object sender, EventArgs e)
    {
        isMoving = false;
        isIdle = false;
        isOpeningHorizontalDoor = true;
        UpdateAnimations();
    }

    private void ItemPickupState_OnEnterStateItemPickup(object sender, EventArgs e)
    {
        isPickingUpItem = true;
        isEnteringVent = false;
        isMoving = false;
        isIdle = false;
        isHidden = false;
        isHiding = false;
        isLeaving = false;
        isClimbingDown = false;
        isClimbingUp = false;
        UpdateAnimations();
    }

    private void UsingLadderState_OnExitVents(object sender, EventArgs e)
    {

        isExitingVents = true;
        isEnteringVent = false;
        isMoving = false;
        isIdle = false;
        isHidden = false;
        isHiding = false;
        isLeaving = false;
        isClimbingDown = false;
        isClimbingUp = false;
        isPickingUpItem = false;
        UpdateAnimations();
    }

    private void UsingLadderState_OnGoIntoVents(object sender, EventArgs e)
    {
        isEnteringVent = true;
        isExitingVents = false;
        isMoving = false;
        isIdle = false;
        isHidden = false;
        isHiding = false;
        isLeaving = false;
        isClimbingDown = false;
        isClimbingUp = false;
        isPickingUpItem = false;
        UpdateAnimations();
    }

    private void OnFinishClimbing(object sender, EventArgs e)
    {
        isIdle = true;
        isClimbingDown = false;
        isClimbingUp = false;
        UpdateAnimations();
    }

    private void OnVentEnterOrLeave(object sender, EventArgs e)
    {
        isInVent = playerStateMachine.currentState.isInVent;
        UpdateAnimations();
    }

    private void Climbing_OnLadderMoveDown(object sender, EventArgs e)
    {
        isEnteringVent = false;
        isMoving = false;
        isIdle = false;
        isHidden = false;
        isHiding = false;
        isLeaving = false;
        isClimbingDown = true;
        isClimbingUp = false;
        isPickingUpItem = false;
        UpdateAnimations();
    }
    private void Climbing_OnLadderMoveUp(object sender, EventArgs e)
    {
        isEnteringVent = false;
        isMoving = false;
        isIdle = false;
        isHidden = false;
        isHiding = false;
        isLeaving = false;
        isClimbingDown = false;
        isClimbingUp = true;
        isPickingUpItem = false;
        UpdateAnimations();
    }

    private void HidingState_OnEnterStateHiding(object sender, PlayerStateTryingToHide.OnEnterStateHidingEventArgs e)
    {
        isMoving = false;
        isIdle = false;
        isHidden = false;
        isHiding = true;
        isLeaving = false;
        isClimbingDown = false;
        isClimbingUp = false;
        isPickingUpItem = false;
        //Debug.Log(e.hideoutFurnitureTag);
        if (e.hideoutFurnitureTag == "Closet")
        {
            isCloset = true;
        }
        else if (e.hideoutFurnitureTag == "Hideout")
        {
            isHideout = true;
        }
        else if (e.hideoutFurnitureTag == "Table")
        {
            isTable = true;
        }
        UpdateAnimations();
    }
    private void LeavingState_OnLeaveStateHiding(object sender, EventArgs e)
    {
        isMoving = false;
        isIdle = false;
        isHidden = false;
        isHiding = false;
        isLeaving = true;
        isClimbingDown = false;
        isClimbingUp = false;
        isPickingUpItem = false;

        UpdateAnimations();
    }
    private void LeavingState_OnTurnOffFurnitureTag(object sender, EventArgs e)
    {
        isHideout = false;
        isCloset = false;
        isTable = false;
        UpdateAnimations();
    }
    private void HidingState_OnEnterStateHidden(object sender, EventArgs e)
    {
        isMoving = false;
        isIdle = false;
        isHiding = false;
        isHidden = true;
        isLeaving = false;
        isClimbingDown = false;
        isClimbingUp = false;
        isPickingUpItem = false;
        UpdateAnimations();
    }

    private void MovingState_OnEnterStateMoving(object sender, EventArgs e)
    {
        isOpeningHorizontalDoor = false;
        isMoving = true;
        isIdle = false;
        isHidden = false;
        isHiding = false;
        isLeaving = false;
        isClimbingDown = false;
        isClimbingUp = false;
        isPickingUpItem = false;
        UpdateAnimations();
    }

    private void IdleState_OnEnterStateIdle(object sender, EventArgs e)
    {
        isOpeningHorizontalDoor = false;
        isMoving = false;
        isIdle = true ;
        isHidden = false;
        isHiding = false;
        isLeaving = false;
        isClimbingDown = false;
        isClimbingUp = false;
        isPickingUpItem = false;
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isIdle", isIdle);
        animator.SetBool("isHidden", isHidden);
        animator.SetBool("isHiding", isHiding);
        animator.SetBool("isLeaving", isLeaving);
        animator.SetBool("isClimbingDown", isClimbingDown);
        animator.SetBool("isClimbingUp", isClimbingUp);
        animator.SetBool("isInVent", isInVent);
        animator.SetBool("isEnteringVent", isEnteringVent);
        animator.SetBool("isExitingVent", isExitingVents);
        animator.SetBool("isPickingUpItem", isPickingUpItem);
        animator.SetBool("isOpeningHorizontalDoor", isOpeningHorizontalDoor);
        animator.SetBool("isHideout", isHideout);
        animator.SetBool("isCloset", isCloset);
        animator.SetBool("isTable", isTable);

    }
}
