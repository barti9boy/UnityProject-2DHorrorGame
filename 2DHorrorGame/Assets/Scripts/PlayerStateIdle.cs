using System;
using UnityEngine;

public class PlayerStateIdle : PlayerStateBase
{
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
