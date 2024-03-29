using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using static PlayerActions;

public class PlayerStateIdle : PlayerStateBase
{
    public PlayerStateIdle(GameObject playerObject) : base(playerObject) 
    {
        rb = playerObject.GetComponent<Rigidbody2D>();
        playerInput = playerObject.GetComponent<PlayerInput>();
        inputManager = playerObject.GetComponent<InputManager>();
        playerTransform = playerObject.GetComponent<Transform>();
        flashlight = playerObject.transform.GetComponentInChildren<FlashlightScript>().gameObject;
        playerGFX = playerObject.transform.GetChild(0).gameObject;
        playerSpriteRenderer = playerGFX.GetComponent<SpriteRenderer>();
        playerInventory = playerObject.GetComponent<PlayerInventory>();

    }

    public event EventHandler OnEnterStateIdle;
    public override void EnterState(PlayerStateMachine player, Collider2D collision = null)
    {
        OnEnterStateIdle?.Invoke(this, EventArgs.Empty);
        if(player.isInVent)
            player.flashlight.ChangeFlashlightPosition(FlashlightScript.FlashlightPosition.VentPosition);
        else
            player.flashlight.ChangeFlashlightPosition(FlashlightScript.FlashlightPosition.StandingPosition);

        //Debug.Log("Hello from idle state");
    }
    public override void UpdateState(PlayerStateMachine player, Collider2D collision = null)
    {
        rb.velocity = new Vector2(0,0);
        if(inputManager.movementInputDirection != 0)
        {
            player.previousState = PlayerStates.idle;
            player.SwitchState(PlayerStates.moving);
        }
        //PlayerActions.Flashlight(player);
    }
    public override void OnCollisionEnter(PlayerStateMachine player, Collision2D collision)
    {
        if(collision.collider.tag == "Monster")
        {
            player.SwitchState(PlayerStates.dead);
        }
    }
    public override void OnTriggerStay(PlayerStateMachine player, Collider2D collision)
    {
        
        PlayerActions.Interact(player, collision);
    }
   
}



