using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerStateBase previousState;
    public PlayerStateBase currentState;
    public PlayerStateIdle idleState;
    public PlayerStateMoving movingState;
    public PlayerStateHiding hidingState;
    public PlayerStateDead deadState;
    public PlayerStateUsingLadder usingLadderState;
    public PlayerStateUsingHorizontalDoor usingHorizontalDoorState;
   
    void Awake()
    {
        idleState = new PlayerStateIdle(gameObject);
        movingState = new PlayerStateMoving(gameObject);
        hidingState = new PlayerStateHiding(gameObject);
        deadState = new PlayerStateDead(gameObject);
        usingLadderState = new PlayerStateUsingLadder(gameObject);
        usingHorizontalDoorState = new PlayerStateUsingHorizontalDoor(gameObject);
        previousState = idleState;
        currentState = idleState;
        currentState.EnterState(this); 
    }


    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerStateBase state, Collider2D collision = null)
    {
        currentState = state;
        state.EnterState(this, collision);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        currentState.OnTriggerStay(this, collision);
    }
}
