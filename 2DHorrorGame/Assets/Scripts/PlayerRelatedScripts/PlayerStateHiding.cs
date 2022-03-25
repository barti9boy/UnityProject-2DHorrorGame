using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActions;

public class PlayerStateHiding : PlayerStateBase
{
    public bool isHidden = false;
    Collider2D hideoutCollider;
    Collider2D playerCollider;

    public PlayerStateHiding(GameObject playerObject) : base(playerObject) 
    {
        rb = playerObject.GetComponent<Rigidbody2D>();
        playerInput = playerObject.GetComponent<PlayerInput>();
        inputManager = playerObject.GetComponent<InputManager>();
        playerTransform = playerObject.GetComponent<Transform>();
        flashlight = playerObject.transform.GetChild(1).gameObject;
        playerGFX = playerObject.transform.GetChild(0).gameObject;
        playerSpriteRenderer = playerGFX.GetComponent<SpriteRenderer>();
    }

    public event EventHandler OnEnterStateHidden;
    public override void EnterState(PlayerStateMachine player, Collider2D collision = null)
    {
        playerCollider = player.GetComponent<CapsuleCollider2D>();
        hideoutCollider = collision;

        playerCollider.enabled = false;
        OnEnterStateHidden?.Invoke(this, EventArgs.Empty);
        isFacingRight = player.previousState.isFacingRight;
        isHidden = false;

}
    public override void UpdateState(PlayerStateMachine player, Collider2D collision = null)
    {
        if (inputManager.isInteractionButtonClicked)
        {
            playerCollider.enabled = true;
            player.previousState = this;
            Debug.Log(hideoutCollider);
            player.SwitchState(player.leavingHideoutState, hideoutCollider);
            inputManager.isInteractionButtonClicked = false;

        }
        PlayerActions.Flashlight(player);
        // Flip();
       


    }
    public override void OnCollisionEnter(PlayerStateMachine player, Collision2D collision)
    {

    }
    public override void OnTriggerStay(PlayerStateMachine player, Collider2D collision)
    {
        PlayerActions.Interact(player, collision);
    }

    /*public void Leave()
    {
        inputManager.isInteractionButtonClicked = false;
        flashlight.transform.Rotate(0.0f, 0.0f, 90.0f);
        if (isFacingRight)
        {
            flashlight.transform.position = new Vector3(playerTransform.position.x + 0.2f, playerTransform.position.y, playerTransform.position.z);
        }
        else if (!isFacingRight)
        {
            flashlight.transform.position = new Vector3(playerTransform.position.x - 0.2f, playerTransform.position.y, playerTransform.position.z);
        }
        playerSpriteRenderer.sortingOrder = 1;
        flashlight.GetComponent<SpriteRenderer>().sortingOrder = 1;
        inputManager.movementInputEnabled = true;
    }*/
}
