using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActions;
using System;

public class PlayerStateUsingVerticalDoor : PlayerStateBase
{
    public event EventHandler OnStartMoving;
    public event EventHandler OnOpeningHorizontalDoor;


    //----------door variables----------//
    private int itemIdToUnlock;
    private float unlockTimeRequired;
    private Vector2 verticalDoorPoint;
    private Vector2 horizontalDoorPointIn;
    private Vector2 horizontalDoorPointOut;
    private Collider2D doorCollider;

    //----------door interaction variables----------//
    private float timer;

    private DoorScript vDoors;
    private bool isChangingRoomUsingVerticalDoor;
    private bool isChangingRoomUsingHorizontalDoor;
    private bool isEnteringAnotherRoom;
    private float velocityDirection;
    private Animator horizontalDoorAnimator;
    private Collider2D horizontalDoorCollider;
    private AnimationClip openingAnimation;


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
        // itemIdToUnlock = collision.GetComponent<DoorScript>().itemIdToUnlock;
        //s unlockTimeRequired = collision.GetComponent<DoorScript>().unlockTimeRequired;
        vDoors = collision.GetComponent<DoorScript>();
        timer = 0;
        verticalDoorPoint = collision.gameObject.transform.GetChild(0).transform.position;
        horizontalDoorPointIn = collision.gameObject.transform.GetChild(1).transform.position;
        horizontalDoorPointOut = collision.gameObject.transform.GetChild(2).transform.position;
        horizontalDoorAnimator = collision.gameObject.transform.GetChild(5).gameObject.GetComponent<Animator>() ;
        horizontalDoorCollider = collision.gameObject.transform.GetChild(5).gameObject.transform.GetChild(0).gameObject.GetComponent<Collider2D>();
        openingAnimation = collision.GetComponent<DoorScript>().doorOpeningAnimation;
        isChangingRoomUsingVerticalDoor = false;
        isChangingRoomUsingHorizontalDoor = false;
        isEnteringAnotherRoom = false;

        if (Math.Abs(player.transform.position.y - verticalDoorPoint.y) < Math.Abs(player.transform.position.y - horizontalDoorPointIn.y)) //we are using vertical door
        {
            if (player.transform.position.x < verticalDoorPoint.x) //we're on left
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
            else if (player.transform.position.x > verticalDoorPoint.x) //we're on right
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
           // doorCollider.enabled = false;
            OnStartMoving?.Invoke(this, EventArgs.Empty);
            isChangingRoomUsingVerticalDoor = true;
        }

        else if (Math.Abs(player.transform.position.y - verticalDoorPoint.y) > Math.Abs(player.transform.position.y - horizontalDoorPointIn.y)) //we are using horizontal door
        {
            if (player.transform.position.x < horizontalDoorPointOut.x) //we're on left
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
            else if (player.transform.position.x > horizontalDoorPointOut.x) //we're on right
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
           // doorCollider.enabled = false;
            OnStartMoving?.Invoke(this, EventArgs.Empty);
            isChangingRoomUsingHorizontalDoor = true;
        }
    }

    // Update is called once per frame
    public override void UpdateState(PlayerStateMachine player, Collider2D collision = null)
    {
        if (isChangingRoomUsingVerticalDoor)
        {
            if (velocityDirection == 1 && playerTransform.position.x < verticalDoorPoint.x)
            {
                rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
                if (Math.Abs(playerTransform.position.x - verticalDoorPoint.x) < 0.25)
                {

                    inputManager.interactionInputEnabled = true;
                    rb.velocity = new Vector2(0, 0);
                    player.transform.position = horizontalDoorPointOut;
                    if (horizontalDoorPointIn.x > horizontalDoorPointOut.x)
                    {
                        
                        velocityDirection = 1;
                        rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
                    }
                    else if (horizontalDoorPointIn.x < horizontalDoorPointOut.x)
                    {
                        playerTransform.Rotate(0, 180, 0);
                        player.isFacingRight = !player.isFacingRight;
                        velocityDirection = -1;
                        rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
                    }
                    OnStartMoving?.Invoke(this, EventArgs.Empty);
                    isEnteringAnotherRoom = true;

                }
                
            }
            else if (velocityDirection == -1 && playerTransform.position.x > verticalDoorPoint.x)
            {
                rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
                if (Math.Abs(playerTransform.position.x - verticalDoorPoint.x) < 0.25)
                {
                    inputManager.interactionInputEnabled = true;
                    rb.velocity = new Vector2(0, 0);
                    player.transform.position = horizontalDoorPointOut;
                    if (horizontalDoorPointIn.x > horizontalDoorPointOut.x)
                    {
                        playerTransform.Rotate(0, 180, 0);
                        player.isFacingRight = !player.isFacingRight;
                        velocityDirection = 1;
                        rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
                    }
                    else if (horizontalDoorPointIn.x < horizontalDoorPointOut.x)
                    {
                        
                        velocityDirection = -1;
                        rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
                    }

                    OnStartMoving?.Invoke(this, EventArgs.Empty);
                    isEnteringAnotherRoom = true;
                }
                //if (Math.Abs(playerTransform.position.x - horizontalDoorPointIn.x) < 0.5)
                //{
                //    Debug.Log(playerTransform.position.x + " " + horizontalDoorPointIn.x);
                //    rb.velocity = new Vector2(0, 0);
                //    isChangingRoomUsingVerticalDoor = false;
                //    inputManager.movementInputEnabled = true;
                //    inputManager.interactionInputEnabled = true;
                //    // doorCollider.enabled = true;
                //    player.previousState = this;
                //    player.SwitchState(player.idleState);
                //}
            }
            horizontalDoorCollider.enabled = false;
            //if (player.photonView.IsMine) vDoors.PlayOpenDoorAnim();
            if (isEnteringAnotherRoom)
            {

                if (Math.Abs(playerTransform.position.x - horizontalDoorPointIn.x) < 0.5)
                {
                    Debug.Log(playerTransform.position.x + " " + horizontalDoorPointIn.x);
                    rb.velocity = new Vector2(0, 0);
                    isChangingRoomUsingVerticalDoor = false;
                    isEnteringAnotherRoom = false;
                    inputManager.movementInputEnabled = true;
                    inputManager.interactionInputEnabled = true;
                    //sdoorCollider.enabled = true;
                    player.previousState = PlayerStates.usingVerticalDoor;
                    player.SwitchState(PlayerStates.idle);
                    horizontalDoorCollider.enabled = true;
                    if (player.photonView.IsMine) vDoors.PlayCloseDoorAnim();
                }
            }
        }
        else if (isChangingRoomUsingHorizontalDoor)
        {
            OnOpeningHorizontalDoor.Invoke(this, EventArgs.Empty);
            if (player.photonView.IsMine) vDoors.PlayOpenDoorAnim();
            WaitUntilAnimated();
            rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
            if (Math.Abs(playerTransform.position.x - horizontalDoorPointOut.x) < 0.25)
            {
                rb.velocity = new Vector2(0, 0);
                player.previousState = PlayerStates.usingVerticalDoor;
                player.SwitchState(PlayerStates.idle);
                player.transform.position = verticalDoorPoint;
                isChangingRoomUsingHorizontalDoor = false;
                inputManager.movementInputEnabled = true;
                inputManager.interactionInputEnabled = true;
                horizontalDoorCollider.enabled = true;
                if (player.photonView.IsMine) vDoors.PlayCloseDoorAnim();
                // doorCollider.enabled = true;
            }
        }
    }
    public void WaitUntilAnimated()
    {
        timer += Time.deltaTime;

        if (timer >= openingAnimation.length)
        {
            horizontalDoorCollider.enabled = false;
            OnStartMoving?.Invoke(this, EventArgs.Empty);
        }
    }
    public override void OnCollisionEnter(PlayerStateMachine player, Collision2D collision)
    {
        if (collision.collider.tag == "Monster")
        {
            player.SwitchState(PlayerStates.dead);
        }
    }
    public override void OnTriggerStay(PlayerStateMachine player, Collider2D collision)
    {

    }
}

