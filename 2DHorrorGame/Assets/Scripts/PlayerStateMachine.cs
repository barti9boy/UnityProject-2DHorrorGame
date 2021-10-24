using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    PlayerStateBase currentState;
    PlayerStateIdle idleState = new PlayerStateIdle();
    PlayerStateMoving movingState = new PlayerStateMoving();
    PlayerStateHiding hidingState = new PlayerStateHiding();
    PlayerStateDead deadState = new PlayerStateDead();
   
    void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
    }


    void Update()
    {
        currentState.UpdateState(this);
    }
}
