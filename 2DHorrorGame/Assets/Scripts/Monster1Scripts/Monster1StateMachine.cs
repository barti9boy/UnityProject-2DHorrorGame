using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1StateMachine : MonoBehaviour
{
    public Monster1StateBase previousState;
    public Monster1StateBase currentState;
    public Monster1StateIdle idleState;
    public Monster1StatePatrolling patrollingState;


    void Awake()
    {
        idleState = new Monster1StateIdle(gameObject);
        patrollingState = new Monster1StatePatrolling(gameObject);
        previousState = idleState;
        currentState = idleState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(Monster1StateBase state, Collider2D collision = null)
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
