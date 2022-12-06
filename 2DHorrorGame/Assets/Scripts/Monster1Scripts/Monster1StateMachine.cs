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
    public bool isFacingRightOnAwake;
    public bool isLookingForAPlayer;


    void Awake()
    {
        idleState = new Monster1StateIdle(gameObject);
        patrollingState = new Monster1StatePatrolling(gameObject);
        chasingState = new Monster1StateChasing(gameObject);
        previousState = idleState;
        currentState = idleState;
        currentState.EnterState(this, hitPlayer);
        currentState.isFacingRight = isFacingRightOnAwake;
        if (!isFacingRightOnAwake)
        {
            transform.Rotate(0,180,0);
        }
        isLookingForAPlayer = false;
    }

    void Update()
    {
        //if (isLookingForAPlayer)
        //{
        if (currentState == chasingState)
        {
            hitPlayer = Physics2D.Raycast(this.transform.position, this.transform.right, 10.0f, playerMask);
            //Debug.DrawRay(this.transform.position, this.transform.right * 10.0f);
            if (hitPlayer)
            {
                if (hitPlayer.collider.gameObject.TryGetComponent(out PlayerStateMachine player))
                {
                    if (player.flashlight.gameObject.activeInHierarchy)
                    {
                        Debug.Log("Player hit front with flashlight on");
                    }
                    else
                    {
                        hitPlayer = Physics2D.Raycast(this.transform.position, this.transform.right, 5.0f, playerMask);
                        if (hitPlayer)
                        {
                            Debug.Log("Player hit front with flashlight off");
                        }
                    }
                }
                else
                {
                    Debug.Log("Raycast hit something other than player");
                }
            }

            else
            {
                hitPlayer = Physics2D.Raycast(this.transform.position, -this.transform.right, 5.0f, playerMask);
                //Debug.DrawRay(this.transform.position, -this.transform.right * 5.0f);
                if (hitPlayer)
                {
                    if (hitPlayer.collider.gameObject.TryGetComponent(out PlayerStateMachine player))
                    {
                        if (player.flashlight.gameObject.activeInHierarchy)
                        {
                            Debug.Log("Player hit back with flashlight on");
                        }
                        else
                        {
                            hitPlayer = Physics2D.Raycast(this.transform.position, this.transform.right, 3.0f, playerMask);
                            if (hitPlayer)
                            {
                                Debug.Log("Player hit back with flashlight off");
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Raycast hit something other than player");
                    }
                }
            }
        }

        else
        {
            hitPlayer = Physics2D.Raycast(this.transform.position, this.transform.right, 8.0f, playerMask);
            //Debug.DrawRay(this.transform.position, this.transform.right * 10.0f);
            if (hitPlayer)
            {
                if (hitPlayer.collider.gameObject.TryGetComponent(out PlayerStateMachine player))
                {
                    if (player.flashlight.gameObject.activeInHierarchy)
                    {
                        Debug.Log("Player hit front with flashlight on");
                    }
                    else
                    {
                        hitPlayer = Physics2D.Raycast(this.transform.position, this.transform.right, 4.0f, playerMask);
                        if (hitPlayer)
                        {
                            Debug.Log("Player hit front with flashlight off");
                        }
                    }
                }
                else
                {
                    Debug.Log("Raycast hit something other than player");
                }
            }

            else
            {
                hitPlayer = Physics2D.Raycast(this.transform.position, -this.transform.right, 3.0f, playerMask);
                //Debug.DrawRay(this.transform.position, -this.transform.right * 5.0f);
                if (hitPlayer)
                {
                    if (hitPlayer.collider.gameObject.TryGetComponent(out PlayerStateMachine player))
                    {
                        if (player.flashlight.gameObject.activeInHierarchy)
                        {
                            Debug.Log("Player hit back with flashlight on");
                        }
                        else
                        {
                            hitPlayer = Physics2D.Raycast(this.transform.position, this.transform.right, 2.0f, playerMask);
                            if (hitPlayer)
                            {
                                Debug.Log("Player hit back with flashlight off");
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Raycast hit something other than player");
                    }
                }
            }
        }
        currentState.UpdateState(this, hitPlayer);


        //}
        //else
        //{
        //    hitPlayer = Physics2D.Raycast(this.transform.position, this.transform.right,0.5f, playerMask);
        //    Debug.DrawRay(this.transform.position, this.transform.right * 0.5f);
        //}
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
