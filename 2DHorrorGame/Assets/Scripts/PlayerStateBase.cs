using UnityEngine;

public abstract class PlayerStateBase
{
    public abstract void EnterState(PlayerStateMachine player);
    public abstract void UpdateState(PlayerStateMachine player);
    public abstract void OnCollisionEnter(PlayerStateMachine player);
}
