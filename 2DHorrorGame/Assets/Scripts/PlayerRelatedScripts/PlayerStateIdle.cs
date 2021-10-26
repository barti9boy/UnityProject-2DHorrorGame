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
    public override void OnCollisionEnter(PlayerStateMachine player)
    {

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
