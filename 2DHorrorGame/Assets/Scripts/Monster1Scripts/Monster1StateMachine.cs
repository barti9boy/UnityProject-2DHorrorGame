using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1StateMachine : MonoBehaviour
{
    [SerializeField] GameObject flashlight;
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
        if (flashlight.activeSelf == true) 
        {
            hitPlayer = Physics2D.Raycast(this.transform.position, this.transform.right, 8.0f, playerMask);
            Debug.DrawRay(this.transform.position, this.transform.right * 8.0f);
            if (hitPlayer.collider == null)
            {
                hitPlayer = Physics2D.Raycast(this.transform.position, -this.transform.right, 3.0f, playerMask);
                Debug.DrawRay(this.transform.position, -this.transform.right * 3.0f);
                //Debug.Log(hitPlayer.collider);
            }
            currentState.UpdateState(this, hitPlayer);
        }
        else
        {
            hitPlayer = Physics2D.Raycast(this.transform.position, this.transform.right, 4.0f, playerMask);
            Debug.DrawRay(this.transform.position, this.transform.right * 4.0f);
            if (hitPlayer.collider == null)
            {
                hitPlayer = Physics2D.Raycast(this.transform.position, -this.transform.right, 2.0f, playerMask);
                Debug.DrawRay(this.transform.position, -this.transform.right * 2.0f);
                //Debug.Log(hitPlayer.collider);
            }
            currentState.UpdateState(this, hitPlayer);
        }
 
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
