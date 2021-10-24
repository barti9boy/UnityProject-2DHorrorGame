using UnityEngine;

public class PlayerStateMoving : PlayerStateBase
{
    public override void EnterState(PlayerStateMachine player)
    {
        Debug.Log("Hello from moving state");
    }
    public override void UpdateState(PlayerStateMachine player)
    {

    }
    public override void OnCollisionEnter(PlayerStateMachine player)
    {

    }
}
