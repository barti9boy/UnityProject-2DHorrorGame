using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGFXScript : MonoBehaviour
{
    public PlayerStateMachine playerSM;
    public PlayerStateIdle idleState;
    public PlayerStateMoving movingState;
    public PlayerStateHiding hidingState;
    public PlayerStateDead deadState;
    public Animator animator;
    private bool isIdle;
    private bool isMoving;
    private bool isHidden;

    private void Start()
    {
        animator = GetComponent<Animator>();

        playerSM = GetComponentInParent<PlayerStateMachine>();
        idleState = playerSM.idleState;
        movingState = playerSM.movingState;
        hidingState = playerSM.hidingState;
        deadState = playerSM.deadState;

        //subscribing to events
        idleState.OnEnterStateIdle += IdleState_OnEnterStateIdle;
        movingState.OnEnterStateMoving += MovingState_OnEnterStateMoving;
        hidingState.OnEnterStateHiding += HidingState_OnEnterStateHiding;
    }

    private void HidingState_OnEnterStateHiding(object sender, EventArgs e)
    {
        isMoving = false;
        isIdle = false;
        isHidden = true;
        UpdateAnimations();
    }

    private void MovingState_OnEnterStateMoving(object sender, EventArgs e)
    {
 
        isMoving = true;
        isIdle = false;
        isHidden = false;
        UpdateAnimations();
    }

    private void IdleState_OnEnterStateIdle(object sender, EventArgs e)
    {
        isMoving = false;
        isIdle = true ;
        isHidden = false;
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isIdle", isIdle);
        animator.SetBool("isHidden", isHidden);
    }
}
