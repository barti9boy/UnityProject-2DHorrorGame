using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActions;

public class PlayerStateIdle : PlayerStateBase
{
    public PlayerStateIdle(GameObject playerObject) : base(playerObject) 
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

    public event EventHandler OnEnterStateIdle;
    public override void EnterState(PlayerStateMachine player, Collider2D collision = null)
    {
        OnEnterStateIdle?.Invoke(this, EventArgs.Empty);
        isFacingRight = player.previousState.isFacingRight;
        isInVent = player.previousState.isInVent;
        //Debug.Log("Hello from idle state");
    }
    public override void UpdateState(PlayerStateMachine player, Collider2D collision = null)
    {
        rb.velocity = new Vector2(0,0);
        if(inputManager.movementInputDirection != 0)
        {
            player.previousState = this;
            player.SwitchState(player.movingState);
        }
        PlayerActions.Flashlight(player);
    }
    public override void OnCollisionEnter(PlayerStateMachine player, Collision2D collision)
    {
        if(collision.collider.tag == "Monster")
        {
            player.SwitchState(player.deadState);
        }
    }
    public override void OnTriggerStay(PlayerStateMachine player, Collider2D collision)
    {
        PlayerActions.Interact(player, collision);
    }
   
}



