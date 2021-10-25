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

    private void Start()
    {
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
        Debug.Log("Playing Movement Animation!");
    }

    private void IdleState_OnEnterStateIdle(object sender, EventArgs e)
    {
        Debug.Log("Playing Idle Animation!");
    }

}
