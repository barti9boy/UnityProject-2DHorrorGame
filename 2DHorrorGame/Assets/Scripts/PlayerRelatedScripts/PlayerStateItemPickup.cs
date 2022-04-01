using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActions;

public class PlayerStateItemPickup : PlayerStateBase
{
    private float timer;
    private AnimationClip pickupAnimation;
    public PlayerStateItemPickup(GameObject playerObject) : base(playerObject)
    {
        rb = playerObject.GetComponent<Rigidbody2D>();
        playerInput = playerObject.GetComponent<PlayerInput>();
        inputManager = playerObject.GetComponent<InputManager>();
        playerTransform = playerObject.GetComponent<Transform>();
        flashlight = playerObject.transform.GetChild(1).gameObject;
        playerGFX = playerObject.transform.GetChild(0).gameObject;
        playerSpriteRenderer = playerGFX.GetComponent<SpriteRenderer>();
        playerInventory = playerObject.GetComponent<PlayerInventory>();

    }

    public event EventHandler OnEnterStateItemPickup;
    public override void EnterState(PlayerStateMachine player, Collider2D collision = null)
    {
        Debug.Log(collision);
        pickupAnimation = collision.GetComponent<IPickableObject>().PickupAnimationClip;
        timer = 0f;
        OnEnterStateItemPickup?.Invoke(this, EventArgs.Empty);
        player.currentState.playerTransform.Rotate(0.0f, 180.0f, 0.0f); //animation is in the wrong direction, that is why we need to flip
        isFacingRight = player.previousState.isFacingRight;
        isInVent = player.previousState.isInVent;
    }
    public override void UpdateState(PlayerStateMachine player, Collider2D collision = null)
    {
        rb.velocity = new Vector2(0, 0);
        timer += Time.deltaTime;
        if (timer >= pickupAnimation.length)
        {
            player.currentState.playerTransform.Rotate(0.0f, 180.0f, 0.0f);
            player.SwitchState(player.previousState);
        }
        PlayerActions.Flashlight(player);
    }
    public override void OnCollisionEnter(PlayerStateMachine player, Collision2D collision)
    {
        if (collision.collider.tag == "Monster")
        {
            player.SwitchState(player.deadState);
        }
    }
    public override void OnTriggerStay(PlayerStateMachine player, Collider2D collision)
    {
        
    }

}
