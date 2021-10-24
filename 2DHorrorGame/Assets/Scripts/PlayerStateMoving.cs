using System;
using UnityEngine;

public class PlayerStateMoving : PlayerStateBase
{
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
