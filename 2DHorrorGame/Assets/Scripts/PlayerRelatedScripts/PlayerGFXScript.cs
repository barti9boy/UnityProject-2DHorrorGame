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
    public bool isIdle;
    public bool isMoving;

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
    }

    private void MovingState_OnEnterStateMoving(object sender, EventArgs e)
    {
        isMoving = true;
        isIdle = false;
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isIdle", isIdle);
    }

    private void IdleState_OnEnterStateIdle(object sender, EventArgs e)
    {
        isMoving = false;
        isIdle = true ;
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isIdle", isIdle);
    }

}
