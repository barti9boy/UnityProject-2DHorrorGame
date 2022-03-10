using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActions;
using System;

public class PlayerStateUsingVerticalDoor : PlayerStateBase
{
    public event EventHandler OnStartMoving;

    //----------door variables----------//
    private int itemIdToUnlock;
    private float unlockTimeRequired;
    private Vector2 verticalDoorPoint;
    private Vector2 horizontalDoorPointIn;
    private Vector2 horizontalDoorPointOut;
    private Collider2D doorCollider;

    //----------door interaction variables----------//
    private bool isChangingRoomUsingVerticalDoor;
    private bool isChangingRoomUsingHorizontalDoor;
    private bool isEnteringAnotherRoom;
    private float velocityDirection;

    public PlayerStateUsingVerticalDoor(GameObject playerObject) : base(playerObject)
    {
        rb = playerObject.GetComponent<Rigidbody2D>();
        playerInput = playerObject.GetComponent<PlayerInput>();
        inputManager = playerObject.GetComponent<InputManager>();
        playerTransform = playerObject.GetComponent<Transform>();
        flashlight = playerObject.transform.GetChild(1).gameObject;
        playerGFX = playerObject.transform.GetChild(0).gameObject;
        playerSpriteRenderer = playerGFX.GetComponent<SpriteRenderer>();
        playerInventory = playerObject.GetComponent<PlayerInventory>();

    }
    public override void EnterState(PlayerStateMachine player, Collider2D collision = null)
    {
       // itemIdToUnlock = collision.GetComponent<DoorScript>().itemIdToUnlock;
       //s unlockTimeRequired = collision.GetComponent<DoorScript>().unlockTimeRequired;
        verticalDoorPoint = collision.gameObject.transform.GetChild(0).transform.position;
        horizontalDoorPointIn = collision.gameObject.transform.GetChild(1).transform.position;
        horizontalDoorPointOut = collision.gameObject.transform.GetChild(2).transform.position;
        isChangingRoomUsingVerticalDoor = false;
        isChangingRoomUsingHorizontalDoor = false;
        isEnteringAnotherRoom = false;
        isFacingRight = player.previousState.isFacingRight;

        if (Math.Abs(player.transform.position.y - verticalDoorPoint.y) < Math.Abs(player.transform.position.y - horizontalDoorPointIn.y)) //we are using vertical door
        {
            if (player.transform.position.x < verticalDoorPoint.x) //we're on left
            {
                if (!player.currentState.isFacingRight)
                {
                    playerTransform.Rotate(0, 180, 0);
                    player.currentState.isFacingRight = !player.currentState.isFacingRight;
                    Debug.Log(player.currentState.isFacingRight);
                    inputManager.isInteractionButtonClicked = false;
                }
                velocityDirection = 1;
            }
            else if (player.transform.position.x > verticalDoorPoint.x) //we're on right
            {
                if (player.currentState.isFacingRight)
                {
                    playerTransform.Rotate(0, 180, 0);
                    player.currentState.isFacingRight = !player.currentState.isFacingRight;
                    Debug.Log(player.currentState.isFacingRight);
                    inputManager.isInteractionButtonClicked = false;
                }
                velocityDirection = -1;
            }
           // doorCollider.enabled = false;
            OnStartMoving?.Invoke(this, EventArgs.Empty);
            isChangingRoomUsingVerticalDoor = true;
        }

        else if (Math.Abs(player.transform.position.y - verticalDoorPoint.y) > Math.Abs(player.transform.position.y - horizontalDoorPointIn.y)) //we are using horizontal door
        {
            if (player.transform.position.x < horizontalDoorPointOut.x) //we're on left
            {
                if (!player.currentState.isFacingRight)
                {
                    playerTransform.Rotate(0, 180, 0);
                    player.currentState.isFacingRight = !player.currentState.isFacingRight;
                    Debug.Log(player.currentState.isFacingRight);
                    inputManager.isInteractionButtonClicked = false;
                }
                velocityDirection = 1;
            }
            else if (player.transform.position.x > horizontalDoorPointOut.x) //we're on right
            {
                if (player.currentState.isFacingRight)
                {
                    playerTransform.Rotate(0, 180, 0);
                    player.currentState.isFacingRight = !player.currentState.isFacingRight;
                    Debug.Log(player.currentState.isFacingRight);
                    inputManager.isInteractionButtonClicked = false;
                }
                velocityDirection = -1;
            }
           // doorCollider.enabled = false;
            OnStartMoving?.Invoke(this, EventArgs.Empty);
            isChangingRoomUsingHorizontalDoor = true;
        }
    }

    // Update is called once per frame
    public override void UpdateState(PlayerStateMachine player, Collider2D collision = null)
    {
        if (isChangingRoomUsingVerticalDoor)
        {
            if (velocityDirection == 1 && playerTransform.position.x < verticalDoorPoint.x)
            {
                rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
                if (Math.Abs(playerTransform.position.x - verticalDoorPoint.x) < 0.1)
                {

                    inputManager.interactionInputEnabled = true;
                    rb.velocity = new Vector2(0, 0);
                    player.transform.position = horizontalDoorPointOut;
                    rb.velocity = new Vector2(1 * movementSpeed, 0);
                    OnStartMoving?.Invoke(this, EventArgs.Empty);
                    isEnteringAnotherRoom = true;

                }
                
            }
            else if (velocityDirection == -1 && playerTransform.position.x > verticalDoorPoint.x)
            {
                rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
                if (Math.Abs(playerTransform.position.x - verticalDoorPoint.x) < 0.1)
                {
                    inputManager.interactionInputEnabled = true;
                    rb.velocity = new Vector2(0, 0);
                    player.transform.position = horizontalDoorPointOut;
                    playerTransform.Rotate(0, 180, 0);
                    player.currentState.isFacingRight = !player.currentState.isFacingRight;
                    rb.velocity = new Vector2(1 * movementSpeed, 0);
                    OnStartMoving?.Invoke(this, EventArgs.Empty);
                    isEnteringAnotherRoom = true;
                }
                //if (Math.Abs(playerTransform.position.x - horizontalDoorPointIn.x) < 0.5)
                //{
                //    Debug.Log(playerTransform.position.x + " " + horizontalDoorPointIn.x);
                //    rb.velocity = new Vector2(0, 0);
                //    isChangingRoomUsingVerticalDoor = false;
                //    inputManager.movementInputEnabled = true;
                //    inputManager.interactionInputEnabled = true;
                //    // doorCollider.enabled = true;
                //    player.previousState = this;
                //    player.SwitchState(player.idleState);
                //}
            }
            if (isEnteringAnotherRoom)
            {

                if (Math.Abs(playerTransform.position.x - horizontalDoorPointIn.x) < 0.5)
                {
                    Debug.Log(playerTransform.position.x + " " + horizontalDoorPointIn.x);
                    rb.velocity = new Vector2(0, 0);
                    isChangingRoomUsingVerticalDoor = false;
                    isEnteringAnotherRoom = false;
                    inputManager.movementInputEnabled = true;
                    inputManager.interactionInputEnabled = true;
                    //sdoorCollider.enabled = true;
                    player.previousState = this;
                    player.SwitchState(player.idleState);
                }
            }
        }
        else if (isChangingRoomUsingHorizontalDoor)
        {
            rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
            if (Math.Abs(playerTransform.position.x - horizontalDoorPointOut.x) < 0.1)
            {
                rb.velocity = new Vector2(0, 0);
                player.previousState = this;
                player.SwitchState(player.idleState);
                player.transform.position = verticalDoorPoint;
                isChangingRoomUsingHorizontalDoor = false;
                inputManager.movementInputEnabled = true;
                inputManager.interactionInputEnabled = true;
               // doorCollider.enabled = true;
            }
        }
    }

    public override void OnCollisionEnter(PlayerStateMachine player, Collision2D collision)
    {

    }
    public override void OnTriggerStay(PlayerStateMachine player, Collider2D collision)
    {

    }
}

