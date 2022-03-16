using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1GFXScript : MonoBehaviour
{
    public Monster1StateMachine monster1StateMachine;
    public Monster1StateIdle idleState;
    public Monster1StatePatrolling patrollingState;
    public Animator animator;
    private bool isIdle;
    private bool isWalking;

    void Start()
    {
        animator = GetComponent<Animator>();

        monster1StateMachine = GetComponentInParent<Monster1StateMachine>();
        idleState = monster1StateMachine.idleState;
        patrollingState = monster1StateMachine.patrollingState;

        //subscribing to events
        idleState.OnEnterStateIdle += IdleState_OnEnterStateIdle;
        patrollingState.OnEnterStatePatrolling += PatrollingState_OnEnterStatePatrolling;
    }
    private void IdleState_OnEnterStateIdle(object sender, EventArgs e)
    {
        isIdle = true;
        isWalking = false;
        UpdateAnimations();
    }
    private void PatrollingState_OnEnterStatePatrolling(object sender, EventArgs e)
    { 
        isIdle = false;
        isWalking = true;
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        animator.SetBool("isIdle", isIdle);
        animator.SetBool("isWalking", isWalking);
    }
}
