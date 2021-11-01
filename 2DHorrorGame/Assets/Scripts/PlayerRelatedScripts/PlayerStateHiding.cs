using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateHiding : PlayerStateBase
{

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

    public event EventHandler OnEnterStateHiding;
    public override void EnterState(PlayerStateMachine player)
    {
        OnEnterStateHiding?.Invoke(this, EventArgs.Empty);
        isFacingRight = player.movingState.isFacingRight;
        Hide();
    }
    public override void UpdateState(PlayerStateMachine player)
    {
        if (inputManager.isInteractionButtonClicked)
        {
            inputManager.isInteractionButtonClicked = false;
            Leave();
            player.SwitchState(player.idleState);
        }
        Flashlight();
       // Flip();

    }
    public override void OnCollisionEnter(PlayerStateMachine player, Collision2D collision)
    {

    }
    public override void OnTriggerStay(PlayerStateMachine player, Collider2D collision)
    {
        
    }
    public void Hide()
    {
        playerSpriteRenderer.sortingOrder = -7;
        flashlight.GetComponent<SpriteRenderer>().sortingOrder = -7;
        rb.velocity = new Vector2(0, 0);
        flashlight.transform.Rotate(0.0f, 0.0f, -90.0f);
        flashlight.transform.position = playerTransform.position;
    }
    public void Leave()
    {
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
    public void Flip()
    {
        if (inputManager.movementInputDirection == 1 && !isFacingRight)
        {
            playerTransform.Rotate(0.0f, 180.0f, 0.0f);
            isFacingRight = true;
        }
        else if (inputManager.movementInputDirection == -1 && isFacingRight)
        {
            playerTransform.Rotate(0.0f, 180.0f, 0.0f);
            isFacingRight = false;
        }
    }
}
