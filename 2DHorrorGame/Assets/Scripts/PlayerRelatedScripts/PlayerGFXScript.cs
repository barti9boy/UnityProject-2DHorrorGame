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
    private bool isHiding;
    private bool isLeaving;


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
        hidingState.OnEnterStateHidden += HidingState_OnEnterStateHidden;
        GameObject.FindGameObjectWithTag("Hideout").GetComponent<HideoutScript>().OnEnterStateHiding += HidingState_OnEnterStateHiding; ;
        GameObject.FindGameObjectWithTag("Hideout").GetComponent<HideoutScript>().OnLeaveStateHiding += HidingState_OnLeaveStateHiding; ;


    }

    private void HidingState_OnEnterStateHiding(object sender, EventArgs e)
    {
        isMoving = false;
        isIdle = false;
        isHidden = false;
        isHiding = true;
        isLeaving = false;
        UpdateAnimations();
    }
    private void HidingState_OnLeaveStateHiding(object sender, EventArgs e)
    {
        isMoving = false;
        isIdle = false;
        isHidden = false;
        isHiding = false;
        isLeaving = true;
        UpdateAnimations();
    }
    private void HidingState_OnEnterStateHidden(object sender, EventArgs e)
    {
        isMoving = false;
        isIdle = false;
        isHiding = false;
        isHidden = true;
        isLeaving = false;
        UpdateAnimations();
    }

    private void MovingState_OnEnterStateMoving(object sender, EventArgs e)
    {
 
        isMoving = true;
        isIdle = false;
        isHidden = false;
        isLeaving = false;
        UpdateAnimations();
    }

    private void IdleState_OnEnterStateIdle(object sender, EventArgs e)
    {
        isMoving = false;
        isIdle = true ;
        isHidden = false;
        isLeaving = false;
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isIdle", isIdle);
        animator.SetBool("isHidden", isHidden);
        animator.SetBool("isHiding", isHiding);
        animator.SetBool("isLeaving", isLeaving);

    }
}
