using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public enum MonsterStates
{
    idle,
    patrolling,
    chasing
}
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

    public PhotonView photonView;
    private Dictionary<MonsterStates, Monster1StateBase> states;

    void Awake()
    {
        idleState = new Monster1StateIdle(gameObject);
        patrollingState = new Monster1StatePatrolling(gameObject);
        chasingState = new Monster1StateChasing(gameObject);
        previousState = idleState;
        currentState = idleState;
        currentState.EnterState(this);
        currentState.isFacingRight = isFacingRightOnAwake;
        photonView = GetComponent<PhotonView>();
        if (!isFacingRightOnAwake)
        {
            transform.Rotate(0, 180, 0);
        }
        isLookingForAPlayer = false;
        CreateDictionary();
    }

    void Update()
    {
        //if (isLookingForAPlayer)
        //{
        if (currentState == chasingState)
        {
            hitPlayer = Physics2D.Raycast(this.transform.position, this.transform.right, 10.0f, playerMask);
            Debug.DrawRay(this.transform.position, this.transform.right * 10.0f);
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
                Debug.DrawRay(this.transform.position, -this.transform.right * 5.0f);
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
            Debug.DrawRay(this.transform.position + new Vector3(0.1f , 0.2f, 0), this.transform.right * 8.0f, Color.green);
            Debug.DrawRay(this.transform.position + new Vector3(0.1f, 0, 0), this.transform.right * 4.0f, Color.red);

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
                Debug.DrawRay(this.transform.position + new Vector3(0, 0.2f, 0), -this.transform.right * 3.0f, Color.green);
                Debug.DrawRay(this.transform.position, -this.transform.right * 2.0f, Color.red);

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
        currentState.UpdateState(this);


        //}
        //else
        //{
        //    hitPlayer = Physics2D.Raycast(this.transform.position, this.transform.right,0.5f, playerMask);
        //    Debug.DrawRay(this.transform.position, this.transform.right * 0.5f);
        //}
    }

    //public void SwitchState(Monster1StateBase state, Collider2D collision = null)
    //{
    //    currentState = state;
    //    state.EnterState(this, collision);
    //}

    public void SwitchState(MonsterStates state, Collider2D collider = null)
    {
        var newState = states[state];

        if (photonView.IsMine)
        {
            int colliderID = collider == null ? 0 : collider.gameObject.GetPhotonView().ViewID;
            photonView.RPC("RPC_SwitchState", RpcTarget.Others, photonView.ViewID, (int)state, colliderID);
            Debug.Log($"Sent state {state}, colliderID {colliderID}");
        }
        currentState = newState;
        newState.EnterState(this, collider);
    }

    [PunRPC]
    private void RPC_SwitchState(int photonID, int state, int colliderID = 0)
    {
        if (photonID == photonView.ViewID)
        {
            Collider2D collider;
            if (colliderID != 0)
            {
                collider = PhotonView.Find(colliderID).GetComponent<Collider2D>();
                Debug.Log($"Collider hit {collider.gameObject.name}");
                SwitchState((MonsterStates)state, collider);
            }
            else
                SwitchState((MonsterStates)state);

            Debug.Log($"Recieved state {(MonsterStates)state}, colliderID {colliderID}");
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        currentState.OnTriggerStay(this, collision);
    }

    private void CreateDictionary()
    {
        states = new Dictionary<MonsterStates, Monster1StateBase>();

        states.Add(MonsterStates.idle, idleState);
        states.Add(MonsterStates.patrolling, patrollingState);
        states.Add(MonsterStates.chasing, chasingState);
    }
}