using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActions;
using System;

public class PlayerStateUsingHorizontalDoor : PlayerStateBase
{
    //----------door variables----------//
    private int itemIdToUnlock;
    private float unlockTimeRequired;
    private float rightPointX;
    private float leftPointX;
    private Collider2D doorCollider;


    //----------door interaction variables----------//
    private bool isChangingRoom;
    private float velocityDirection;

    public PlayerStateUsingHorizontalDoor(GameObject playerObject) : base(playerObject)
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
        itemIdToUnlock = collision.GetComponent<DoorScript>().itemIdToUnlock;
        unlockTimeRequired = collision.GetComponent<DoorScript>().unlockTimeRequired;
        doorCollider = collision.gameObject.transform.GetChild(0).GetComponent<Collider2D>();
        rightPointX = collision.gameObject.transform.GetChild(1).transform.position.x;
        leftPointX = collision.gameObject.transform.GetChild(2).transform.position.x;
        isChangingRoom = false;
        isFacingRight = player.previousState.isFacingRight;

        if (player.transform.position.x < collision.transform.position.x) //jeste�my po lewej
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
        else if (player.transform.position.x > collision.transform.position.x) // jeste�my po prawej
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
        doorCollider.enabled = false;
        isChangingRoom = true;
    }
    public override void UpdateState(PlayerStateMachine player, Collider2D collision = null)
    {
        if(isChangingRoom)
        {
            if (velocityDirection == 1 && playerTransform.position.x < rightPointX)
            {
                rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
                if (Math.Abs(playerTransform.position.x - rightPointX) < 0.1)
                {
                    isChangingRoom = false;
                    inputManager.movementInputEnabled = true;
                    inputManager.interactionInputEnabled = true;
                    doorCollider.enabled = true;
                    player.previousState = this;
                    player.SwitchState(player.idleState);
                }
            }
            if (velocityDirection == -1 && playerTransform.position.x > leftPointX)
            {
                rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
                if (Math.Abs(playerTransform.position.x - leftPointX) < 0.1)
                {
                    isChangingRoom = false;
                    inputManager.movementInputEnabled = true;
                    inputManager.interactionInputEnabled = true;
                    doorCollider.enabled = true;
                    player.previousState = this;
                    player.SwitchState(player.idleState);
                }
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
