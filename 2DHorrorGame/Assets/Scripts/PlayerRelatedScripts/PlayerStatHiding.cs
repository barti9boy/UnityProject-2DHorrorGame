using System;
using UnityEngine;

public class PlayerStateHiding : PlayerStateBase
{
    public PlayerStateHiding(GameObject playerObject) : base(playerObject) { }

    public event EventHandler OnEnterStateHiding;
    public override void EnterState(PlayerStateMachine player)
    {
        OnEnterStateHiding?.Invoke(this, EventArgs.Empty);
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
