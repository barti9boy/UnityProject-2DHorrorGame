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
    private LadderScript ladder;
    private bool isApproachingLadder;
    private bool isGoingUp;
    private bool isGoingDown;
    private float velocityDirection;

    //------------player gfx events and variables------------//
    public event EventHandler OnStartMoving;
    public event EventHandler OnLadderMoveUp;
    public event EventHandler OnLadderMoveDown;
    public event EventHandler OnVentEnterOrLeave;
    public event EventHandler OnFinishClimbing;
    public event EventHandler OnGoIntoVents;
    private float timer;
    private bool isTimerOn;
    public AnimationClip usingVentEntranceAnimation;


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
        ladder = collision.GetComponent<LadderScript>();
        timer = 0;
        usingVentEntranceAnimation = collision.GetComponent<LadderScript>().usingVentEntraceAnimation;
        //on EnterState get ladder components, set up ladder and player variables 
        isApproachingLadder = false;
        isGoingUp = false;
        isGoingDown = false;
        isVentEntrance = ladder.isEntrance;
        ladderMiddleX = collision.gameObject.transform.position.x;
        ladderMiddleY = collision.gameObject.transform.position.y;
        ladderUpPointY = ladder.UpPoint.transform.position.y;
        ladderDownPointY = ladder.DownPoint.transform.position.y;

        if (playerTransform.position.x < ladderMiddleX)
        {
            if (!player.isFacingRight)
            {
                playerTransform.Rotate(0, 180, 0);
                player.isFacingRight = !player.isFacingRight;
                inputManager.isInteractionButtonClicked = false;
            }
            velocityDirection = 1;
        }
        else if (playerTransform.position.x > ladderMiddleX)
        {
            if (player.isFacingRight)
            {
                playerTransform.Rotate(0, 180, 0);
                player.isFacingRight = !player.isFacingRight;
                inputManager.isInteractionButtonClicked = false;
            }
            velocityDirection = -1;
        }
        isApproachingLadder = true;
        OnStartMoving?.Invoke(this, EventArgs.Empty);
        //Debug.Log("ladderstate");
    }
    public override void UpdateState(PlayerStateMachine player, Collider2D collision = null)
    {
        if(isTimerOn)
        {
            WaitUntilAnimated();
        }
        if(isApproachingLadder)
        {
            if (velocityDirection == 1 && playerTransform.position.x < ladderMiddleX)
            {
                rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
                if (Math.Abs(playerTransform.position.x - ladderMiddleX) < 0.25)
                {
                    PrepareToUseLadder(player);
                }
            }
            if (velocityDirection == -1 && playerTransform.position.x > ladderMiddleX)
            {
                rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
                if (Math.Abs(playerTransform.position.x - ladderMiddleX) < 0.25)
                {
                    PrepareToUseLadder(player);
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
                    player.isInVent = !player.isInVent;
                    OnVentEnterOrLeave?.Invoke(this, EventArgs.Empty);
                }
                OnFinishClimbing?.Invoke(this, EventArgs.Empty);
                if (player.isFacingRight)
                {
                    flashlight.transform.position = new Vector3(playerTransform.position.x + 0.3f, playerTransform.position.y - 0.9f, playerTransform.position.z);
                }
                else if (!player.isFacingRight)
                {
                    flashlight.transform.position = new Vector3(playerTransform.position.x - 0.3f, playerTransform.position.y - 0.9f, playerTransform.position.z);
                }
                playerTransform.position = new Vector2(playerTransform.position.x, ladderDownPointY);
                player.previousState = States.usingLadder;
                player.SwitchState(States.idle);
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
                    player.isInVent = !player.isInVent;
                    OnVentEnterOrLeave?.Invoke(this, EventArgs.Empty);
                }
                OnFinishClimbing?.Invoke(this, EventArgs.Empty);
                if (player.isFacingRight)
                {
                    flashlight.transform.position = new Vector3(playerTransform.position.x + 0.375f, playerTransform.position.y - 0.4f, playerTransform.position.z);
                }
                else if (!player.isFacingRight)
                {
                    flashlight.transform.position = new Vector3(playerTransform.position.x - 0.375f, playerTransform.position.y - 0.4f, playerTransform.position.z);
                }
                playerTransform.position = new Vector2(playerTransform.position.x, ladderUpPointY);
                player.previousState = States.usingLadder;
                player.SwitchState(States.idle);
            }
        }
    }
    public void PrepareToUseLadder(PlayerStateMachine player)
    {
        flashlight.transform.Rotate(0.0f, 0.0f, -90.0f);
        isApproachingLadder = false;
        if (playerTransform.position.y > ladderMiddleY)
        {
            if (isVentEntrance) 
            {
                OnGoIntoVents?.Invoke(this, EventArgs.Empty);
                isTimerOn = true;


            }
            else
            {
                if (player.photonView.IsMine) ladder.PlayOpenAnim();
                isGoingDown = true;
            }
            
        }
        if (playerTransform.position.y < ladderMiddleY) isGoingUp = true;
        rb.velocity = new Vector2(0f, 0f);
    }


    public void WaitUntilAnimated()
    {
        timer += Time.deltaTime;

        if (timer >= usingVentEntranceAnimation.length)
        {
            isGoingDown = true;
            timer = 0;
            isTimerOn = false;
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
}
