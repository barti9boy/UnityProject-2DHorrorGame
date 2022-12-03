using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActions;

public class PlayerStateHiding : PlayerStateBase
{
    private float timer;
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
        timer = 0;
        playerCollider = player.GetComponent<CapsuleCollider2D>();
        hideoutCollider = collision;
        OnEnterStateHidden?.Invoke(this, EventArgs.Empty);
        isFacingRight = player.isFacingRight;
        isHidden = false;
}
    public override void UpdateState(PlayerStateMachine player, Collider2D collision = null)
    {
        if (!flashlight.activeSelf)
        {
            WaitBeforeDisactivatingCollider(player, 0.2f);
        }
        else if (flashlight.activeSelf)
        {
            playerCollider.enabled = true;
        }

        if (inputManager.isInteractionButtonClicked)
        {
            playerCollider.enabled = true;
            player.previousState = States.hiding;
            player.SwitchState(States.leavingHideout, hideoutCollider);
            inputManager.isInteractionButtonClicked = false;

        }
        PlayerActions.Flashlight(player);
        // Flip();
       


    }
    public override void OnCollisionEnter(PlayerStateMachine player, Collision2D collision)
    {
        if (collision.collider.tag == "Monster")
        {
            player.SwitchState(States.dead);
        }
    }
    public override void OnTriggerStay(PlayerStateMachine player, Collider2D collision)
    {
        PlayerActions.Interact(player, collision);
    }

    public void WaitBeforeDisactivatingCollider(PlayerStateMachine player, float timeOfAnimation)
    {
        timer += Time.deltaTime;
        if (timer >= timeOfAnimation)
        {
            playerCollider.enabled = false;
            timer = 0;
        }
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
