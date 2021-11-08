using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateHiding : PlayerStateBase
{
    public bool isHidden = false;
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
        isFacingRight = player.previousState.isFacingRight;
        isHidden = false;

}
    public override void UpdateState(PlayerStateMachine player)
    {
        if (inputManager.isInteractionButtonClicked)
        {
            inputManager.isInteractionButtonClicked = false;
            Leave();
            player.previousState = this;
            player.SwitchState(player.idleState);
        }
        Flashlight();
        // Flip();
        if (collider != null)
        {
            Hiding(player, collider);
        }


    }
    public override void OnCollisionEnter(PlayerStateMachine player, Collision2D collision)
    {

    }
    public override void OnTriggerStay(PlayerStateMachine player, Collider2D collision)
    {
        if (!isHidden)
        {
            collider = collision;
            isHidden = true;
        }
    }
    public void Hiding(PlayerStateMachine player, Collider2D collision)
    {
        if (playerTransform.position.x != collision.transform.position.x)
        {
            if (playerTransform.position.x - collision.transform.position.x < 0)
            {
                rb.velocity = new Vector2(movementSpeed * 1 / 2, 0);
            }
            else if (playerTransform.position.x - collision.transform.position.x > 0)
            {
                rb.velocity = new Vector2(movementSpeed * -1 / 2, 0);
            }
        }
        if (Math.Abs(playerTransform.position.x - collision.transform.position.x) < 0.1)
        {
            collider = null;
            Hide();
        }

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
