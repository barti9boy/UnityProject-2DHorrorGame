using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateIdle : PlayerStateBase
{
    public Rigidbody2D rb;
    public PlayerInput playerInput;
    public PlayerActions playerActions;

    public PlayerStateIdle(GameObject playerObject) : base(playerObject) 
    {
        rb = playerObject.GetComponent<Rigidbody2D>();
        playerInput = playerObject.GetComponent<PlayerInput>();
        playerActions = playerObject.GetComponent<PlayerActions>();
    }

    public event EventHandler OnEnterStateIdle;
    public override void EnterState(PlayerStateMachine player)
    {
        OnEnterStateIdle?.Invoke(this, EventArgs.Empty);
        Debug.Log("Hello from idle state");
    }
    public override void UpdateState(PlayerStateMachine player)
    {
        rb.velocity = new Vector2(0,0);
        if(playerActions.movementInputDirection != 0)
        {
            player.SwitchState(player.movingState);
        }
    }
    public override void OnCollisionEnter(PlayerStateMachine player)
    {

    }
}
