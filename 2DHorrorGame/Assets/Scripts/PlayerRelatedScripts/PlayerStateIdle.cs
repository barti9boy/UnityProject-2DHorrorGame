using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateIdle : PlayerStateBase
{
    public PlayerStateIdle(UnityEngine.Object playerObject) : base(playerObject) {}

    public event EventHandler OnEnterStateIdle;
    public override void EnterState(PlayerStateMachine player)
    {
        OnEnterStateIdle?.Invoke(this, EventArgs.Empty);
        Debug.Log("Hello from idle state");
    }
    public override void UpdateState(PlayerStateMachine player)
    {
        if (Input.GetButtonDown("Jump"))
        {
            player.SwitchState(player.movingState);
        }

    }
    public override void OnCollisionEnter(PlayerStateMachine player)
    {

    }
}
