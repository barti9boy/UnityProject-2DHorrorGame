using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActions;

public class PlayerStateMoving : PlayerStateBase
{
    public Rigidbody2D rb;
    public PlayerInput playerInput;
    public PlayerStateMoving(UnityEngine.Object playerObject) : base(playerObject) 
    {
        rb = playerObject.Rigidbody2D;
    }

    public event EventHandler OnEnterStateMoving;
    public override void EnterState(PlayerStateMachine player)
    {
        OnEnterStateMoving?.Invoke(this, EventArgs.Empty);
        Debug.Log("Hello from moving state");
        Move();
    }
    public override void UpdateState(PlayerStateMachine player)
    {

    }
    public override void OnCollisionEnter(PlayerStateMachine player)
    {

    }
}
