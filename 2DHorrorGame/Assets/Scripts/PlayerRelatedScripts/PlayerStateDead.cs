using System;
using UnityEngine;

public class PlayerStateDead : PlayerStateBase
{
    public PlayerStateDead(GameObject playerObject) : base(playerObject) { }

    public event EventHandler OnEnterStateDead;
    public override void EnterState(PlayerStateMachine player)
    {
        OnEnterStateDead?.Invoke(this, EventArgs.Empty);
    }
    public override void UpdateState(PlayerStateMachine player)
    {

    }
    public override void OnCollisionEnter(PlayerStateMachine player, Collision2D collision)
    {

    }
    public override void OnTriggerStay(PlayerStateMachine player, Collider2D collision)
    {

    }
}
