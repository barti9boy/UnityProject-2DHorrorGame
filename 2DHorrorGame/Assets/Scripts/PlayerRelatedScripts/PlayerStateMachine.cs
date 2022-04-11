using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject flashlight;
    public bool flashlightOutOfBattery;
    public float batteryTimer;
    private float timeOfBattery;

   
    void Awake()
    {
        flashlight = transform.GetChild(1).gameObject;
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
        timeOfBattery = 5;
        batteryTimer = 0;
    }


    void Update()
    {
        currentState.UpdateState(this);
        if(currentState == deadState)
        {
            GameOverScreen.GameOver();
        }
        if (flashlight.activeSelf == true)
        {
            batteryTimer += Time.deltaTime;
            if (batteryTimer >= timeOfBattery)
            {
                flashlightOutOfBattery = true;
                currentState.flashlight.SetActive(false);
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
