using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActions;

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
    public override void EnterState(PlayerStateMachine player, Collider2D collision = null)
    {
        OnEnterStateMoving?.Invoke(this, EventArgs.Empty);
        if(player.isInVent)
            player.flashlight.ChangeFlashlightPosition(FlashlightScript.FlashlightPosition.VentPosition);
        else
        {
            player.flashlight.ChangeFlashlightPosition(FlashlightScript.FlashlightPosition.WalkingPosition);
        }
        //Debug.Log("Hello from moving state");
    }
    public override void UpdateState(PlayerStateMachine player, Collider2D collision = null)
    {
        if(inputManager.movementInputDirection == 0)
        {
            player.previousState = PlayerStates.moving;
            player.SwitchState(PlayerStates.idle);
        }
        else
        {
            rb.velocity = new Vector2(movementSpeed * inputManager.movementInputDirection, 0);
        }
        PlayerActions.Flip(player);
        //PlayerActions.Flashlight(player);
    }
    public override void OnCollisionEnter(PlayerStateMachine player, Collision2D collision)
    {
        if (collision.collider.tag == "Monster")
        {
            player.SwitchState(PlayerStates.dead);
        }
    }
    public override void OnTriggerStay(PlayerStateMachine player, Collider2D collision)
    {
        PlayerActions.Interact(player, collision);
    }
}
