using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActions;
using System;

public class PlayerStateUsingVerticalDoor : PlayerStateBase
{

    public PlayerStateUsingVerticalDoor(GameObject playerObject) : base(playerObject)
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
    public override void EnterState(PlayerStateMachine player, Collider2D collision = null)
    {

    }

    // Update is called once per frame
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

