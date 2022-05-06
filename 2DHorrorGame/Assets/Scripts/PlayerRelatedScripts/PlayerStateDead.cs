using System;
using UnityEngine;

public class PlayerStateDead : PlayerStateBase
{
    public PlayerStateDead(GameObject playerObject) : base(playerObject) 
    {
        rb = playerObject.GetComponent<Rigidbody2D>();
        inputManager = playerObject.GetComponent<InputManager>();
        playerTransform = playerObject.GetComponent<Transform>();
        flashlight = playerObject.transform.GetChild(1).gameObject;
        playerGFX = playerObject.transform.GetChild(0).gameObject;
        playerSpriteRenderer = playerGFX.GetComponent<SpriteRenderer>();
    }

    public event EventHandler OnEnterStateDead;
    public override void EnterState(PlayerStateMachine player, Collider2D collision = null)
    {
        rb.velocity = new Vector2(0f, 0f);
        OnEnterStateDead?.Invoke(this, EventArgs.Empty);
        isFacingRight = player.movingState.isFacingRight;
    }
    public override void UpdateState(PlayerStateMachine player, Collider2D collision = null)
    {

    }
    public override void OnCollisionEnter(PlayerStateMachine player, Collision2D collision)
    {

    }
    public override void OnTriggerStay(PlayerStateMachine player, Collider2D collision)
    {

    }
}
