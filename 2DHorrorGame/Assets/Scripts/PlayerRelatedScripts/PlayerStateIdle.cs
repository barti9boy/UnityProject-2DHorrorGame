using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateIdle : PlayerStateBase
{

    public PlayerStateIdle(GameObject playerObject) : base(playerObject) 
    {
        rb = playerObject.GetComponent<Rigidbody2D>();
        playerInput = playerObject.GetComponent<PlayerInput>();
        inputManager = playerObject.GetComponent<InputManager>();
        playerTransform = playerObject.GetComponent<Transform>();
        flashlight = playerObject.transform.GetChild(1).gameObject;
        playerInventory = playerObject.GetComponent<PlayerInventory>();
    }

    public event EventHandler OnEnterStateIdle;
    public override void EnterState(PlayerStateMachine player)
    {
        OnEnterStateIdle?.Invoke(this, EventArgs.Empty);
        //Debug.Log("Hello from idle state");
    }
    public override void UpdateState(PlayerStateMachine player)
    {
        rb.velocity = new Vector2(0,0);
        if(inputManager.movementInputDirection != 0)
        {
            player.SwitchState(player.movingState);
        }
        Flashlight();

    }
    public override void OnCollisionEnter(PlayerStateMachine player, Collision2D collision)
    {
        
    }
    public override void OnTriggerStay(PlayerStateMachine player, Collider2D collision)
    {
        if (inputManager.isInteractionButtonClicked)
        {
            if(collision.CompareTag("Key"))
            {
                playerInventory.AddItemToInventory(collision.gameObject.GetComponent<KeyScript>().ItemID);
                collision.gameObject.SetActive(false);
                playerInventory.DebugLogInventory();
            }
            if(collision.CompareTag("Doors"))
            {
                collision.gameObject.GetComponent<DoorScript>().DoorInteraction(playerInventory.inventoryItemsIDs);
            }
        }
    }
    public void Flashlight()
    {
        if (inputManager.isFlashlightButtonClicked)
        {
            flashlight.SetActive(true);
        }
        else if (!inputManager.isFlashlightButtonClicked)
        {
            flashlight.SetActive(false);
        }
    }

}



