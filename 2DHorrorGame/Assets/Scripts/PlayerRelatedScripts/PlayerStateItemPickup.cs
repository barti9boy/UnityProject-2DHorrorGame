using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActions;

public class PlayerStateItemPickup : PlayerStateBase
{
    private float timer;
    private bool canPickupItem;
    private Collider2D item;
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
        pickupAnimation = collision.GetComponent<IPickableObject>().PickupAnimationClip;
        timer = 0f;
        inputManager.movementInputEnabled = false;
        OnEnterStateItemPickup?.Invoke(this, EventArgs.Empty);
        item = collision;
        //if (!collision.CompareTag("Battery"))
        //{
        canPickupItem = player.currentState.playerInventory.AddItemToInventory(collision.gameObject.GetComponent<IPickableObject>());
        if(collision.TryGetComponent(out BatteryScript battery))
        {
            player.currentState.playerInventory.AddBateryToInventory();
            collision.gameObject.SetActive(false);
            player.flashlightOutOfBattery = false;
        }
        //}
    }
    public override void UpdateState(PlayerStateMachine player, Collider2D collision = null)
    {
        rb.velocity = new Vector2(0, 0);
        timer += Time.deltaTime;
        if(timer >= pickupAnimation.length/1.66 && canPickupItem)
        {
            item.gameObject.SetActive(false);
            canPickupItem = false;
        }
        if (timer >= pickupAnimation.length)
        {
            player.SwitchState(player.previousState);
            inputManager.movementInputEnabled = true;
        }
        //PlayerActions.Flashlight(player);
        PlayerActions.Flip(player);
    }
    public override void OnCollisionEnter(PlayerStateMachine player, Collision2D collision)
    {
        if (collision.collider.tag == "Monster")
        {
            inputManager.movementInputEnabled = true;
            player.SwitchState(PlayerStates.dead);
        }
    }
    public override void OnTriggerStay(PlayerStateMachine player, Collider2D collision)
    {
        
    }
    public void AddToInventory(PlayerStateMachine player, Collider2D collision)
    {
        if (player.currentState.playerInventory.AddItemToInventory(collision.gameObject.GetComponent<IPickableObject>()))
        {
            collision.gameObject.SetActive(false);
        }
        
    }

}
