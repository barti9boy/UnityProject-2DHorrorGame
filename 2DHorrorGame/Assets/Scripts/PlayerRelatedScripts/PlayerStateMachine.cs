using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public enum PlayerStates
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
    //public GameOverScript GameOverScreen;
    public PlayerStates previousState;
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

    public InputManager inputManager;
    public FlashlightScript flashlight;
    public bool flashlightOutOfBattery;
    public float batteryTimer;
    public float timeOfBattery;
    public bool isFacingRight = true;
    public bool isInVent = false;

    public PhotonView photonView;
    private Dictionary<PlayerStates, PlayerStateBase> states;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        flashlight = transform.GetComponentInChildren<FlashlightScript>();
        inputManager = GetComponent<InputManager>();
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
        previousState = PlayerStates.idle;
        currentState = idleState;
        currentState.EnterState(this);
        flashlightOutOfBattery = false;
        timeOfBattery = 10;
        batteryTimer = 0;
        CreateDictionary();
    }

    private void Start()
    {
        flashlight.gameObject.SetActive(false);
    }


    void Update()
    {
        currentState.UpdateState(this);
        //if (currentState == deadState)
        //{
        //    GameOverScreen.GameOver();
        //}
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
                SwitchState((PlayerStates)state, collider);
            }
            else
                SwitchState((PlayerStates)state);

            Debug.Log($"Recieved state {(PlayerStates)state}, colliderID {colliderID}");
        }
    }
    public void SwitchState(PlayerStates state, Collider2D collider = null)
    {
        var newState = states[state];

        if (photonView.IsMine)
        {
            int colliderID = collider == null ? 0 : collider.gameObject.GetPhotonView().ViewID;
            photonView.RPC("RPC_SwitchState", RpcTarget.Others, photonView.ViewID, (int)state, colliderID);
            Debug.Log($"Sent state {state}, colliderID {colliderID}");
        }

        if(state == PlayerStates.idle || state == PlayerStates.moving)
        {
            inputManager.inventoryButtonEnabled = true;
        }
        else
        {
            inputManager.inventoryButtonEnabled = false;
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
        states = new Dictionary<PlayerStates, PlayerStateBase>();

        states.Add(PlayerStates.idle, idleState);
        states.Add(PlayerStates.moving, movingState);
        states.Add(PlayerStates.hiding, hidingState);
        states.Add(PlayerStates.tryingToHide, tryingToHideState);
        states.Add(PlayerStates.leavingHideout, leavingHideoutState);
        states.Add(PlayerStates.dead, deadState);
        states.Add(PlayerStates.usingLadder, usingLadderState);
        states.Add(PlayerStates.usingHorizontalDoor, usingHorizontalDoorState);
        states.Add(PlayerStates.usingVerticalDoor, usingVerticalDoorState);
        states.Add(PlayerStates.itemPickup, itemPickupState);
    }
}


