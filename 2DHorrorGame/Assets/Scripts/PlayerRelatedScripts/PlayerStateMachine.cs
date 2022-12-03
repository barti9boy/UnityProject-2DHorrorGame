using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public enum States
{
    idle,
    moving,
    hiding,
    tryingToHide,
    leavingHideout,
    dead,
    usingLadder,
    usingHorizontalDoor,
    usingVerticalDoor,
    itemPickup
}

public class PlayerStateMachine : MonoBehaviour
{
    public GameOverScript GameOverScreen;
    public States previousState;
    public PlayerStateBase currentState;
    public PlayerStateIdle idleState;
    public PlayerStateMoving movingState;
    public PlayerStateHiding hidingState;
    public PlayerStateTryingToHide tryingToHideState;
    public PlayerStateLeavingHideout leavingHideoutState;
    public PlayerStateDead deadState;
    public PlayerStateUsingLadder usingLadderState;
    public PlayerStateUsingHorizontalDoor usingHorizontalDoorState;
    public PlayerStateUsingVerticalDoor usingVerticalDoorState;
    public PlayerStateItemPickup itemPickupState;

    public FlashlightScript flashlight;
    public bool flashlightOutOfBattery;
    public float batteryTimer;
    public float timeOfBattery;
    public bool isFacingRight = true;
    public bool isInVent = false;

    private PhotonView photonView;

   
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        flashlight = transform.GetComponentInChildren<FlashlightScript>();
        idleState = new PlayerStateIdle(gameObject);
        movingState = new PlayerStateMoving(gameObject);
        hidingState = new PlayerStateHiding(gameObject);
        tryingToHideState = new PlayerStateTryingToHide(gameObject);
        leavingHideoutState = new PlayerStateLeavingHideout(gameObject);
        deadState = new PlayerStateDead(gameObject);
        usingLadderState = new PlayerStateUsingLadder(gameObject);
        usingHorizontalDoorState = new PlayerStateUsingHorizontalDoor(gameObject);
        usingVerticalDoorState = new PlayerStateUsingVerticalDoor(gameObject);
        itemPickupState = new PlayerStateItemPickup(gameObject);
        previousState = States.idle;
        currentState = idleState;
        currentState.EnterState(this);
        flashlightOutOfBattery = false;
        timeOfBattery = 40;
        batteryTimer = 0;
    }


    void Update()
    {
        currentState.UpdateState(this);
        if(currentState == deadState)
        {
            GameOverScreen.GameOver();
        }
        if (flashlight.gameObject.activeSelf == true)
        {
            batteryTimer += Time.deltaTime;
            if (batteryTimer >= timeOfBattery)
            {
                if(currentState.playerInventory.PlayerBatteries != 0)
                {
                    currentState.playerInventory.ChangeBattery();
                    batteryTimer = 0;
                }
                else
                {
                    flashlightOutOfBattery = true;
                    currentState.flashlight.SetActive(false);
                }

            }
        }

    }

    [PunRPC]
    private void RPC_SwitchState(int photonID, int state)
    {
        if(photonID == photonView.ViewID)
        {
            SwitchState((States) state);
        }
    }
    public PlayerStateBase GetState(States state)
    {
        PlayerStateBase newState;
        switch (state)
        {
            case States.idle:
                {
                    newState = idleState;
                    break;
                }
            case States.moving:
                {
                    newState = movingState;
                    break;
                }
            case States.hiding:
                {
                    newState = hidingState;
                    break;
                }
            case States.tryingToHide:
                {
                    newState = tryingToHideState;
                    break;
                }
            case States.leavingHideout:
                {
                    newState = leavingHideoutState;
                    break;
                }
            case States.dead:
                {
                    newState = deadState;
                    break;
                }
            case States.usingLadder:
                {
                    newState = usingLadderState;
                    break;
                }
            case States.usingHorizontalDoor:
                {
                    newState = usingHorizontalDoorState;
                    break;
                }
            case States.usingVerticalDoor:
                {
                    newState = usingVerticalDoorState;
                    break;
                }
            case States.itemPickup:
                {
                    newState = itemPickupState;
                    break;
                }
            default:
                {
                    newState = idleState;
                    break;
                }

        }
        return newState;
    }
    public void SwitchState(States state, Collider2D collision = null)
    {
        var newState = GetState(state);

        if (photonView.IsMine)
            photonView.RPC("RPC_SwitchState", RpcTarget.Others, photonView.GetInstanceID(), (int)state);

        currentState = newState;
        newState.EnterState(this, collision);
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
