using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActions;
using System;

public class PlayerStateUsingLadder : PlayerStateBase
{
    //----------ladder variables----------//
    private bool isVentEntrance;
    private float ladderMiddleX;
    private float ladderMiddleY;
    private float ladderUpPointY;
    private float ladderDownPointY;

    //------------ladder interaction phases variables------------//
    private bool isApproachingLadder;
    private bool isGoingUp;
    private bool isGoingDown;
    private float velocityDirection;

    //------------player gfx events------------//
    public event EventHandler OnLadderMoveUp;
    public event EventHandler OnLadderMoveDown;
    public event EventHandler OnVentEnterOrLeave;
    public event EventHandler OnFinishClimbing;

    public PlayerStateUsingLadder(GameObject playerObject) : base(playerObject)
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
        //on EnterState get ladder components, set up ladder and player variables 
        isApproachingLadder = false;
        isGoingDown = false;
        isGoingDown = false;
        isVentEntrance = collision.GetComponent<LadderScript>().IsEntrance;
        ladderMiddleX = collision.gameObject.transform.position.x;
        ladderMiddleY = collision.gameObject.transform.position.y;
        ladderUpPointY = collision.GetComponent<LadderScript>().UpPoint.transform.position.y;
        ladderDownPointY = collision.GetComponent<LadderScript>().DownPoint.transform.position.y;
        isFacingRight = player.previousState.isFacingRight;
        isInVent = player.previousState.isInVent;

        if (playerTransform.position.x < ladderMiddleX)
        {
            if (!player.currentState.isFacingRight)
            {
                playerTransform.Rotate(0, 180, 0);
                player.currentState.isFacingRight = !player.currentState.isFacingRight;
                Debug.Log(player.currentState.isFacingRight);
                inputManager.isInteractionButtonClicked = false;
            }
            velocityDirection = 1;
        }
        else if (playerTransform.position.x > ladderMiddleX)
        {
            if (player.currentState.isFacingRight)
            {
                playerTransform.Rotate(0, 180, 0);
                player.currentState.isFacingRight = !player.currentState.isFacingRight;
                Debug.Log(player.currentState.isFacingRight);
                inputManager.isInteractionButtonClicked = false;
            }
            velocityDirection = -1;
        }
        isApproachingLadder = true;
        Debug.Log("ladderstate");
    }
    public override void UpdateState(PlayerStateMachine player, Collider2D collision = null)
    {
        if(isApproachingLadder)
        {
            if (velocityDirection == 1 && playerTransform.position.x < ladderMiddleX)
            {
                rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
                if (Math.Abs(playerTransform.position.x - ladderMiddleX) < 0.1)
                {
                    flashlight.transform.Rotate(0.0f, 0.0f, -90.0f);
                    isApproachingLadder = false;
                    if (playerTransform.position.y > ladderMiddleY) isGoingDown = true;
                    if (playerTransform.position.y < ladderMiddleY) isGoingUp = true;
                    rb.velocity = new Vector2(0f, 0f);
                }
            }
            if (velocityDirection == -1 && playerTransform.position.x > ladderMiddleX)
            {
                rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
                if (Math.Abs(playerTransform.position.x - ladderMiddleX) < 0.1)
                {
                    flashlight.transform.Rotate(0.0f, 0.0f, -90.0f);
                    isApproachingLadder = false;
                    if (playerTransform.position.y > ladderMiddleY) isGoingDown = true;
                    if (playerTransform.position.y < ladderMiddleY) isGoingUp = true;
                    rb.velocity = new Vector2(0f, 0f);
                }
            }
        }
        if(isGoingDown)
        {
            OnLadderMoveDown?.Invoke(this, EventArgs.Empty);
            if (playerTransform.position.y > ladderDownPointY)
            {
                rb.velocity = new Vector2(0, -1f * movementSpeed);
            }
            if (playerTransform.position.y < ladderDownPointY)
            {
                playerTransform.position = new Vector2(playerTransform.position.x, ladderDownPointY);
                isGoingDown = false;
                isGoingUp = false;
                inputManager.movementInputEnabled = true;
                inputManager.interactionInputEnabled = true;
                flashlight.transform.Rotate(0.0f, 0.0f, 90.0f);
                if (isVentEntrance)
                {
                    isInVent = !isInVent;
                    OnVentEnterOrLeave?.Invoke(this, EventArgs.Empty);
                }
                OnFinishClimbing?.Invoke(this, EventArgs.Empty);
                player.previousState = this;
                player.SwitchState(player.idleState);
            }
        }
        if(isGoingUp)
        {
            OnLadderMoveUp?.Invoke(this, EventArgs.Empty);
            if (playerTransform.position.y < ladderUpPointY)
            {
                rb.velocity = new Vector2(0, 1f * movementSpeed);
            }
            if (playerTransform.position.y > ladderUpPointY)
            {
                playerTransform.position = new Vector2(playerTransform.position.x, ladderUpPointY);
                isGoingUp = false;
                isGoingDown = false;
                inputManager.movementInputEnabled = true;
                inputManager.interactionInputEnabled = true;
                flashlight.transform.Rotate(0.0f, 0.0f, 90.0f);
                if (isVentEntrance)
                {
                    isInVent = !isInVent;
                    OnVentEnterOrLeave?.Invoke(this, EventArgs.Empty);
                }
                OnFinishClimbing?.Invoke(this, EventArgs.Empty);
                player.previousState = this;
                player.SwitchState(player.idleState);
            }

        }
    }
    public override void OnCollisionEnter(PlayerStateMachine player, Collision2D collision)
    {
        
    }
    public override void OnTriggerStay(PlayerStateMachine player, Collider2D collision)
    {
        
    }
}
