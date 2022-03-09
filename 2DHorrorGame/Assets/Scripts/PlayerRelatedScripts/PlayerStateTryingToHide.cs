using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActions;
using System;

public class PlayerStateTryingToHide : PlayerStateBase
{
    Collider2D hideoutCollider;

    //hideout variables
    private float hideoutEntrence;
    private Animator hideoutAnimator;
    public AnimationClip hidingAnimation;


    //hiding phases variables
    public bool isApproachingHideout = false;
    public bool isHiding = false;
    public bool isHidden = false;
    public bool isTryingToHide = false;
    private float velocityDirection;
    private float timer;


    //player GFX events
    public event EventHandler OnEnterStateHiding;
    public event EventHandler OnLeaveStateHiding;

    public PlayerStateTryingToHide(GameObject playerObject) : base(playerObject)
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
        //on EnterState get hideout components, set up hideout and player variables 
        hideoutEntrence = collision.GetComponent<HideoutScript>().handle.transform.position.x;
        hidingAnimation = collision.GetComponent<HideoutScript>().hiding;
        hideoutAnimator = collision.GetComponent<Animator>();
        isFacingRight = player.previousState.isFacingRight;
        timer = 0;

        hideoutCollider = collision;

        if (hideoutEntrence > playerTransform.position.x) //jesteœmy po lewej
        {
            if (!player.currentState.isFacingRight)
            {
                playerTransform.Rotate(0, 180, 0);
                player.currentState.isFacingRight = !player.currentState.isFacingRight;
                Debug.Log(player.currentState.isFacingRight);
                player.currentState.inputManager.isInteractionButtonClicked = false;
            }
            velocityDirection = 1;
        }
        else if (hideoutEntrence < playerTransform.position.x) // jesteœmy po prawej
        {
            if (player.currentState.isFacingRight)
            {
                playerTransform.Rotate(0, 180, 0);
                player.currentState.isFacingRight = !player.currentState.isFacingRight;
                Debug.Log(player.currentState.isFacingRight);

                player.currentState.inputManager.isInteractionButtonClicked = false;
            }
            velocityDirection = -1;
        }
        isApproachingHideout = true;
    }

    public override void UpdateState(PlayerStateMachine player, Collider2D collision = null)
    {
        if (isApproachingHideout)
        {
            if (velocityDirection == 1 && playerTransform.position.x < hideoutEntrence)
            {
                rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
                if (Math.Abs(playerTransform.position.x - hideoutEntrence) < 0.1)
                {
                    isApproachingHideout = false;
                    inputManager.interactionInputEnabled = true;
                    OnEnterStateHiding?.Invoke(this, EventArgs.Empty);
                    rb.velocity = new Vector2(0, 0);
                    isTryingToHide = true;

                }
            }
            if (velocityDirection == -1 && playerTransform.position.x > hideoutEntrence)
            {
                rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
                if (Math.Abs(playerTransform.position.x - hideoutEntrence) < 0.1)
                {
                    isApproachingHideout = false;
                    inputManager.interactionInputEnabled = true;
                    OnEnterStateHiding?.Invoke(this, EventArgs.Empty);
                    rb.velocity = new Vector2(0, 0);
                    isTryingToHide = true;
                }
            }
        }
        if (isTryingToHide)
        {
            hideoutAnimator.SetBool("isHiding", true);
            OnEnterStateHiding?.Invoke(this, EventArgs.Empty);
            Debug.Log("HI1");
            WaitUntilAnimated();
        }
        if (isHiding)
        {
            playerSpriteRenderer.sortingOrder = -7;
            flashlight.GetComponent<SpriteRenderer>().sortingOrder = -7;
            flashlight.transform.Rotate(0.0f, 0.0f, -90.0f);
            flashlight.transform.position = playerTransform.position;
            hideoutAnimator.SetBool("isHiding", false);
            hideoutAnimator.SetBool("isHidden", true);
            player.previousState = this;
            player.SwitchState(player.hidingState, hideoutCollider);
            Debug.Log(hideoutCollider);
            isHiding = false;
        }
    }

    public void WaitUntilAnimated()
    {
        timer += Time.deltaTime;
        Debug.Log(hidingAnimation.length);
        Debug.Log(timer);
        if (timer >= hidingAnimation.length)
        {
            isTryingToHide = false;
            isHiding = true;
            Debug.Log("HI2");
        }
    }



    public override void OnCollisionEnter(PlayerStateMachine player, Collision2D collision)
    {

    }
    public override void OnTriggerStay(PlayerStateMachine player, Collider2D collision)
    {

    }
}
