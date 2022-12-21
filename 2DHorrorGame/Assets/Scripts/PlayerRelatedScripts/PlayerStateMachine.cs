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

    public PhotonView photonView;
    private Dictionary<States, PlayerStateBase> states =
    new Dictionary<States, PlayerStateBase>();

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
        timeOfBattery = 10;
        batteryTimer = 0;
        CreateDictionary();

    }

    private void Start()
    {
        flashlight.TurnFlashlightOnOff(false);
    }


    void Update()
    {
        currentState.UpdateState(this);
        if (currentState == deadState)
        {
            GameOverScreen.GameOver();
        }
        if (flashlight.gameObject.activeSelf == true)
        {
            batteryTimer += Time.deltaTime;
            if (batteryTimer >= timeOfBattery)
            {
                if (currentState.playerInventory.PlayerBatteries != 0)
                {
                    currentState.playerInventory.TryChangeBattery();
                    batteryTimer = 0;
                }
                else
                {
                    flashlightOutOfBattery = true;
                    flashlight.TurnFlashlightOnOff(false);
                }

            }
        }

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
                SwitchState((States)state, collider);
            }
            else
                SwitchState((States)state);

            Debug.Log($"Recieved state {(States)state}, colliderID {colliderID}");
        }
    }
    public void SwitchState(States state, Collider2D collider = null)
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
        states.Add(States.idle, idleState);
        states.Add(States.moving, movingState);
        states.Add(States.hiding, hidingState);
        states.Add(States.tryingToHide, tryingToHideState);
        states.Add(States.leavingHideout, leavingHideoutState);
        states.Add(States.dead, deadState);
        states.Add(States.usingLadder, usingLadderState);
        states.Add(States.usingHorizontalDoor, usingHorizontalDoorState);
        states.Add(States.usingVerticalDoor, usingVerticalDoorState);
        states.Add(States.itemPickup, itemPickupState);
    }
}


