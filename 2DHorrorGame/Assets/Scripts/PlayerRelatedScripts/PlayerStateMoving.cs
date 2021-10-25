using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMoving : PlayerStateBase
{
    public Rigidbody2D rb;
    public PlayerInput playerInput;
    public PlayerActions playerActions;
    public PlayerStateMoving(GameObject playerObject) : base(playerObject) 
    {
        rb = playerObject.GetComponent<Rigidbody2D>();
        playerInput = playerObject.GetComponent<PlayerInput>();
        playerActions = playerObject.GetComponent<PlayerActions>();
    }

    public event EventHandler OnEnterStateMoving;
    public override void EnterState(PlayerStateMachine player)
    {
        OnEnterStateMoving?.Invoke(this, EventArgs.Empty);
        Debug.Log("Hello from moving state");
    }
    public override void UpdateState(PlayerStateMachine player)
    {

    }
    public override void OnCollisionEnter(PlayerStateMachine player)
    {

    }
}
