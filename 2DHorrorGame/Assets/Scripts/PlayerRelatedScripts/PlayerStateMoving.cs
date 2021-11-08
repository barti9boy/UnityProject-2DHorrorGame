using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMoving : PlayerStateBase
{

    public PlayerStateMoving(GameObject playerObject) : base(playerObject) 
    {
        rb = playerObject.GetComponent<Rigidbody2D>();
        playerInput = playerObject.GetComponent<PlayerInput>();
        inputManager = playerObject.GetComponent<InputManager>();
        playerTransform = playerObject.GetComponent<Transform>();
        flashlight = playerObject.transform.GetChild(1).gameObject;
        playerInventory = playerObject.GetComponent<PlayerInventory>();
    }

    public event EventHandler OnEnterStateMoving;
    public override void EnterState(PlayerStateMachine player)
    {
        OnEnterStateMoving?.Invoke(this, EventArgs.Empty);
        isFacingRight = player.previousState.isFacingRight;
        //Debug.Log("Hello from moving state");
    }
    public override void UpdateState(PlayerStateMachine player)
    {
        if(inputManager.movementInputDirection == 0)
        {
            player.previousState = this;
            player.SwitchState(player.idleState);
        }
        else
        {
            rb.velocity = new Vector2(movementSpeed * inputManager.movementInputDirection, 0);
        }
        Flip();
        Flashlight();
    }
    public override void OnCollisionEnter(PlayerStateMachine player, Collision2D collision)
    {

    }
    public override void OnTriggerStay(PlayerStateMachine player, Collider2D collision)
    {
        if (inputManager.isInteractionButtonClicked)
        {
            if (collision.CompareTag("Key"))
            {
                playerInventory.AddItemToInventory(collision.gameObject.GetComponent<KeyScript>().ItemID);
                collision.gameObject.SetActive(false);
                playerInventory.DebugLogInventory();
            }
            if (collision.CompareTag("Hideout"))
            {
                inputManager.inputEnabled = false;
                if (playerTransform.position.x - collision.transform.position.x < 0)
                {
                    if (!isFacingRight)
                    {
                        playerTransform.Rotate(0.0f, 180.0f, 0.0f);
                        isFacingRight = true;
                    }
                }
                else if (playerTransform.position.x - collision.transform.position.x > 0)
                {
                    if (isFacingRight)
                    {
                        playerTransform.Rotate(0.0f, 180.0f, 0.0f);
                        isFacingRight = false;
                    }
                }
                player.previousState = this;
                inputManager.isInteractionButtonClicked = false;
                player.SwitchState(player.hidingState);

            }
        }
        if (collision.CompareTag("Doors"))
        {
            if (collision.gameObject.GetComponent<DoorScript>().isLocked)
            {
                collision.gameObject.GetComponent<DoorScript>().DoorUnlock(playerInventory.inventoryItemsIDs, inputManager.isInteractionButtonHeld);
            }
            else
            {
                if (inputManager.isInteractionButtonClicked)
                {
                    collision.gameObject.GetComponent<DoorScript>().DoorOpen();
                    collision.gameObject.GetComponent<DoorScript>().ChangeRoom(playerTransform, rb, inputManager, player);
                }
            }
        }
    }
    public void Flip()
    {
        if (inputManager.movementInputDirection == 1 && !isFacingRight)
        {
            playerTransform.Rotate(0.0f, 180.0f,0.0f);
            isFacingRight = true;
        }
        else if (inputManager.movementInputDirection == -1 && isFacingRight)
        {
            playerTransform.Rotate(0.0f, 180.0f, 0.0f);
            isFacingRight = false;
        }
    }

    public void Flashlight()
    {
        if(inputManager.isFlashlightButtonClicked)
        {
            flashlight.SetActive(true);
        }
        else if (!inputManager.isFlashlightButtonClicked )
        {
            flashlight.SetActive(false);
        }
    }
}
