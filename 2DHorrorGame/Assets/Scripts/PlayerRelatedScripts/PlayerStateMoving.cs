using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMoving : PlayerStateBase
{
    public Rigidbody2D rb;
    public PlayerInput playerInput;
    public InputManager inputManager;
    public Transform playerTransform;

    private bool isFacingRight = true;
    public float movementSpeed = 5f;

    public PlayerStateMoving(GameObject playerObject) : base(playerObject) 
    {
        rb = playerObject.GetComponent<Rigidbody2D>();
        playerInput = playerObject.GetComponent<PlayerInput>();
        inputManager = playerObject.GetComponent<InputManager>();
        playerTransform = playerObject.GetComponent<Transform>();
    }

    public event EventHandler OnEnterStateMoving;
    public override void EnterState(PlayerStateMachine player)
    {
        OnEnterStateMoving?.Invoke(this, EventArgs.Empty);
        //Debug.Log("Hello from moving state");
    }
    public override void UpdateState(PlayerStateMachine player)
    {
        if(inputManager.movementInputDirection == 0)
        {
            player.SwitchState(player.idleState);
        }
        else
        {
            rb.velocity = new Vector2(movementSpeed * inputManager.movementInputDirection, 0);
        }
        Flip();
    }
    public override void OnCollisionEnter(PlayerStateMachine player)
    {

    }
    public void Flip()
    {
        if (inputManager.movementInputDirection == 1 && !isFacingRight)
        {
            playerTransform.Rotate(0.0f, 0.0f, 180.0f);
            isFacingRight = true;
        }
        else if (inputManager.movementInputDirection == -1 && isFacingRight)
        {
            playerTransform.Rotate(0.0f, 0.0f, 180.0f);
            isFacingRight = false;
        }
    }
}
