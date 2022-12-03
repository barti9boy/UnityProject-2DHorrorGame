using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActions;
using System;

public class PlayerStateUsingHorizontalDoor : PlayerStateBase
{

    public event EventHandler OnStartMoving;
    public event EventHandler OnOpeningHorizontalDoor;

    //----------door variables----------//
    private int itemIdToUnlock;
    private float unlockTimeRequired;
    private float rightPointX;
    private float leftPointX;
    private Collider2D doorCollider;
    private Animator doorAnimator;


    //----------door interaction variables----------//
    private bool isChangingRoom;
    private float velocityDirection;
    public AnimationClip openingAnimation;
    private float timer;
    private IInteractible interactible;


    public PlayerStateUsingHorizontalDoor(GameObject playerObject) : base(playerObject)
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
        timer = 0;
        doorAnimator = collision.GetComponent<Animator>();
        openingAnimation = collision.GetComponent<DoorScript>().doorOpeningAnimation;
        itemIdToUnlock = collision.GetComponent<DoorScript>().itemIdToUnlock;
        unlockTimeRequired = collision.GetComponent<DoorScript>().unlockTimeRequired;
        doorCollider = collision.gameObject.transform.GetChild(0).GetComponent<Collider2D>();
        rightPointX = collision.gameObject.transform.GetChild(1).transform.position.x;
        leftPointX = collision.gameObject.transform.GetChild(2).transform.position.x;
        interactible = collision.gameObject.GetComponent<IInteractible>();
        isChangingRoom = false;

        if (player.transform.position.x < collision.transform.position.x) //jesteœmy po lewej
        {
            if (!player.isFacingRight)
            {
                playerTransform.Rotate(0, 180, 0);
                player.isFacingRight = !player.isFacingRight;
                Debug.Log(player.isFacingRight);
                inputManager.isInteractionButtonClicked = false;
            }
            velocityDirection = 1;
        }
        else if (player.transform.position.x > collision.transform.position.x) // jesteœmy po prawej
        {
            if (player.isFacingRight)
            {
                playerTransform.Rotate(0, 180, 0);
                player.isFacingRight = !player.isFacingRight;
                Debug.Log(player.isFacingRight);
                inputManager.isInteractionButtonClicked = false;
            }
            velocityDirection = -1;
        }
        OnOpeningHorizontalDoor.Invoke(this, EventArgs.Empty);
        doorAnimator.SetBool("isOpened", true);
        

    }
    public override void UpdateState(PlayerStateMachine player, Collider2D collision = null)
    {
        WaitUntilAnimated(); // wait for the door opening animation to finish
        if (isChangingRoom)
        {
            if (velocityDirection == 1 && playerTransform.position.x < rightPointX)
            {
                rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
                if (Math.Abs(playerTransform.position.x - rightPointX) < 0.25)
                {
                    isChangingRoom = false;
                    interactible.EnableInteractionHighlight();
                    doorAnimator.SetBool("isOpened", false);
                    inputManager.movementInputEnabled = true;
                    inputManager.interactionInputEnabled = true;
                    doorCollider.enabled = true;
                    player.previousState = States.tryingToHide;
                    player.SwitchState(States.idle);
                }
            }
            if (velocityDirection == -1 && playerTransform.position.x > leftPointX)
            {
                rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
                if (Math.Abs(playerTransform.position.x - leftPointX) < 0.25)
                {
                    isChangingRoom = false;
                    interactible.EnableInteractionHighlight();
                    doorAnimator.SetBool("isOpened", false);
                    inputManager.movementInputEnabled = true;
                    inputManager.interactionInputEnabled = true;
                    doorCollider.enabled = true;
                    player.previousState = States.tryingToHide;
                    player.SwitchState(States.idle);
                }
            }
        }
    }

    public override void OnCollisionEnter(PlayerStateMachine player, Collision2D collision)
    {
        if (collision.collider.tag == "Monster")
        {
            player.SwitchState(States.dead);
        }
    }
    public override void OnTriggerStay(PlayerStateMachine player, Collider2D collision)
    {

    }

    public void WaitUntilAnimated()
    {
        timer += Time.deltaTime;

        if (timer >= openingAnimation.length)
        {
            doorCollider.enabled = false;
            OnStartMoving?.Invoke(this, EventArgs.Empty);
            isChangingRoom = true;
        }
    }
}
