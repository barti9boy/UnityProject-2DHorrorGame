using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1StateMachine : MonoBehaviour
{
    [SerializeField] LayerMask playerMask;
    public Monster1StateBase previousState;
    public Monster1StateBase currentState;
    public Monster1StateIdle idleState;
    public Monster1StatePatrolling patrollingState;
    public Monster1StateChasing chasingState;
    public RaycastHit2D hitPlayer;


    void Awake()
    {
        idleState = new Monster1StateIdle(gameObject);
        patrollingState = new Monster1StatePatrolling(gameObject);
        chasingState = new Monster1StateChasing(gameObject);
        previousState = idleState;
        currentState = idleState;
        currentState.EnterState(this, hitPlayer);
    }

    void Update()
    {
        hitPlayer = Physics2D.Raycast(this.transform.position , this.transform.right, 8.0f, playerMask);
        Debug.DrawRay(this.transform.position , this.transform.right * 8.0f);
        if (hitPlayer.collider != null)
        {
          //  Debug.Log(hitPlayer.transform.position);
        }
        currentState.UpdateState(this, hitPlayer);
    }

    public void SwitchState(Monster1StateBase state, RaycastHit2D hitPlayer, Collider2D collision = null)
    {
        currentState = state;
        state.EnterState(this, hitPlayer, collision);
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
