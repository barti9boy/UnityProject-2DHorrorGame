using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1GFXScript : MonoBehaviour
{
    public Monster1StateMachine monster1StateMachine;
    public Monster1StateIdle idleState;
    public Animator animator;
    private bool isIdle;

    void Start()
    {
        animator = GetComponent<Animator>();

        monster1StateMachine = GetComponentInParent<Monster1StateMachine>();
        idleState = monster1StateMachine.idleState;

        //subscribing to events
        idleState.OnEnterStateIdle += IdleState_OnEnterStateIdle;
    }
    private void IdleState_OnEnterStateIdle(object sender, EventArgs e)
    {
        isIdle = true;
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        animator.SetBool("isIdle", isIdle);
    }
}
