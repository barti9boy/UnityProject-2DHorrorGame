using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerStateMachine : MonoBehaviour
{
    public GameOverScript GameOverScreen;
    public PlayerStateBase previousState;
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
        previousState = idleState;
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
