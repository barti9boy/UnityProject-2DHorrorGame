using System;
using UnityEngine;

public class PlayerStateDead : PlayerStateBase
{
    public event EventHandler OnEnterStateDead;
    public override void EnterState(PlayerStateMachine player)
    {
        OnEnterStateDead?.Invoke(this, EventArgs.Empty);
    }
    public override void UpdateState(PlayerStateMachine player)
    {

    }
    public override void OnCollisionEnter(PlayerStateMachine player)
    {

    }
}
