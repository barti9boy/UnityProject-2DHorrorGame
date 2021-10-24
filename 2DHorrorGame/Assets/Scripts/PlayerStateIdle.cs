using UnityEngine;

public class PlayerStateIdle : PlayerStateBase
{
    public override void EnterState(PlayerStateMachine player)
    {
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
