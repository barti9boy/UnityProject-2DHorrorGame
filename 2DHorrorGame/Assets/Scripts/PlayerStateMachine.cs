using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerStateBase currentState;
    public PlayerStateIdle idleState = new PlayerStateIdle();
    public PlayerStateMoving movingState = new PlayerStateMoving();
    public PlayerStateHiding hidingState = new PlayerStateHiding();
    public PlayerStateDead deadState = new PlayerStateDead();
   
    void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
    }


    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerStateBase state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
