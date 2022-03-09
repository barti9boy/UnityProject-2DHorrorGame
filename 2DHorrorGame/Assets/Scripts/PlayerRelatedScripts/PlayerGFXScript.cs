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
    public Animator animator;
    private bool isIdle;
    private bool isMoving;
    private bool isHidden;
    private bool isHiding;
    private bool isLeaving;
    private bool isClimbingDown;
    private bool isClimbingUp;
    private bool isInVent;


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

        //subscribing to events
        idleState.OnEnterStateIdle += IdleState_OnEnterStateIdle;
        movingState.OnEnterStateMoving += MovingState_OnEnterStateMoving;
        hidingState.OnEnterStateHidden += HidingState_OnEnterStateHidden;
        usingLadderState.OnLadderMoveDown += Climbing_OnLadderMoveDown;
        usingLadderState.OnLadderMoveUp += Climbing_OnLadderMoveDown;
        usingLadderState.OnFinishClimbing += OnFinishClimbing;
        usingLadderState.OnVentEnterOrLeave += OnVentEnterOrLeave;
        tryingToHideState.OnEnterStateHiding += HidingState_OnEnterStateHiding;
        leavingHideoutState.OnLeaveStateHiding += LeavingState_OnLeaveStateHiding;
        


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
        isMoving = false;
        isIdle = false;
        isHidden = false;
        isHiding = false;
        isLeaving = false;
        isClimbingDown = true;
        isClimbingUp = false;
        UpdateAnimations();
    }
    private void Climbing_OnLadderMoveUp(object sender, EventArgs e)
    {
        isMoving = false;
        isIdle = false;
        isHidden = false;
        isHiding = false;
        isLeaving = false;
        isClimbingDown = false;
        isClimbingUp = true;
        UpdateAnimations();
    }

    private void HidingState_OnEnterStateHiding(object sender, EventArgs e)
    {
        isMoving = false;
        isIdle = false;
        isHidden = false;
        isHiding = true;
        isLeaving = false;
        isClimbingDown = false;
        isClimbingUp = false;
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
        UpdateAnimations();
    }

    private void MovingState_OnEnterStateMoving(object sender, EventArgs e)
    {
 
        isMoving = true;
        isIdle = false;
        isHidden = false;
        isHiding = false;
        isLeaving = false;
        isClimbingDown = false;
        isClimbingUp = false;
        UpdateAnimations();
    }

    private void IdleState_OnEnterStateIdle(object sender, EventArgs e)
    {
        isMoving = false;
        isIdle = true ;
        isHidden = false;
        isHiding = false;
        isLeaving = false;
        isClimbingDown = false;
        isClimbingUp = false;
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
    }
}
