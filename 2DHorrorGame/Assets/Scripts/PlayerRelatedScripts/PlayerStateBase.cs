using UnityEngine;

public abstract class PlayerStateBase
{
    public PlayerStateBase(GameObject playerObject)
    {

    }
    public abstract void EnterState(PlayerStateMachine player);
    public abstract void UpdateState(PlayerStateMachine player);
    public abstract void OnCollisionEnter(PlayerStateMachine player);
}
