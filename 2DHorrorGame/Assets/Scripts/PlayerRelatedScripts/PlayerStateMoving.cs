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
    public override void EnterState(PlayerStateMachine player)
    {
        OnEnterStateMoving?.Invoke(this, EventArgs.Empty);
        isFacingRight = player.previousState.isFacingRight;
        isInVent = player.previousState.isInVent;
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
        PlayerActions.Flip(player);
        PlayerActions.Flashlight(player);
    }
    public override void OnCollisionEnter(PlayerStateMachine player, Collision2D collision)
    {

    }
    public override void OnTriggerStay(PlayerStateMachine player, Collider2D collision)
    {
        PlayerActions.Interact(player, collision);
    }
}
