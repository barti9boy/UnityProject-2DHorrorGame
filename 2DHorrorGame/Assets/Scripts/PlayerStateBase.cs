using UnityEngine;

public abstract class PlayerStateBase
{
    abstract void EnterState(PlayerStateMachine player)
    {

    }
    abstract void UpdateState(PlayerStateMachine player)
    {

    }
    abstract void OnCollisionEnter(PlayerStateMachine player)
    {

    }
}
