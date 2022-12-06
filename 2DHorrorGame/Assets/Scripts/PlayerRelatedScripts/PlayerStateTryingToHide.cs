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
    private HideoutScript hideout;
    


    //hiding phases variables
    public bool isApproachingHideout = false;
    public bool isHiding = false;
    public bool isHidden = false;
    public bool isTryingToHide = false;
    private float velocityDirection;
    private float timer;
    private string hideoutTag;


    //player GFX events
    public event EventHandler OnEnterStateMoving;
    public event EventHandler<OnEnterStateHidingEventArgs> OnEnterStateHiding;
    public class OnEnterStateHidingEventArgs : EventArgs
    {
        public string hideoutFurnitureTag;
    }

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
        hideout = collision.GetComponent<HideoutScript>();
        hideoutEntrence = hideout.handle.transform.position.x;
        hideoutTag = collision.tag;
        timer = 0;

        hideoutCollider = collision;

        if (hideoutEntrence > playerTransform.position.x) //jesteœmy po lewej
        {
            if (!player.isFacingRight)
            {
                playerTransform.Rotate(0, 180, 0);
                player.isFacingRight = !player.isFacingRight;
                player.currentState.inputManager.isInteractionButtonClicked = false;
            }
            velocityDirection = 1;
        }
        else if (hideoutEntrence < playerTransform.position.x) // jesteœmy po prawej
        {
            if (player.isFacingRight)
            {
                playerTransform.Rotate(0, 180, 0);
                player.isFacingRight = !player.isFacingRight;
                player.currentState.inputManager.isInteractionButtonClicked = false;
            }
            velocityDirection = -1;
        }
        isApproachingHideout = true;
        OnEnterStateMoving?.Invoke(this, EventArgs.Empty);

    }

    public override void UpdateState(PlayerStateMachine player, Collider2D collision = null)
    {
        if (isApproachingHideout)
        {
            //    Debug.Log(player.currentState.isFacingRight);

            if (velocityDirection == 1 && playerTransform.position.x < hideoutEntrence)
            {
                rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
                if (Math.Abs(playerTransform.position.x - hideoutEntrence) < 0.25)
                {
                    if (hideoutTag == "Hideout" && !player.isFacingRight)
                    {
                        playerTransform.Rotate(0, 180, 0);
                        player.isFacingRight = !player.isFacingRight;
                    }
                    isApproachingHideout = false;
                    inputManager.interactionInputEnabled = true;
                    rb.velocity = new Vector2(0, 0);
                    isTryingToHide = true;

                }
            }
            if (velocityDirection == -1 && playerTransform.position.x > hideoutEntrence)
            {
                rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
                if (Math.Abs(playerTransform.position.x - hideoutEntrence) < 0.25)
                {
                    if( hideoutTag == "Closet" && !player.isFacingRight)
                    {
                        playerTransform.Rotate(0, 180, 0);
                        player.isFacingRight = !player.isFacingRight;
                    }
                    else if (hideoutTag == "Hideout"  && !player.isFacingRight)
                    {
                        playerTransform.Rotate(0, 180, 0);
                        player.isFacingRight = !player.isFacingRight;
                    }
                    isApproachingHideout = false;
                    inputManager.interactionInputEnabled = true;
                    rb.velocity = new Vector2(0, 0);
                    isTryingToHide = true;
                }
            }
        }
        if (isTryingToHide)
        {
            StartGettingInside(player);
            isTryingToHide = false;
        }
    }

    private void StartGettingInside(PlayerStateMachine player)
    {
        if (player.photonView.IsMine)
            hideout.PlayHidingAnim();

        OnEnterStateHiding?.Invoke(this, new OnEnterStateHidingEventArgs { hideoutFurnitureTag = hideoutTag });
        CoroutineHandler.Instance.StartCoroutine(CoroutineHandler.Instance.WaitUntilAnimated(hideout.hiding.length, () => GetInside(player)));
    }

    private void GetInside(PlayerStateMachine player)
    {
        if (hideoutTag == "Table")
        {
            flashlight.transform.Rotate(0.0f, 0.0f, -90.0f);
            flashlight.transform.position = playerTransform.position - new Vector3(0.0f, 0.5f, 0.0f);
            player.previousState = States.tryingToHide;
            player.SwitchState(States.hiding, hideoutCollider);
            isHiding = false;
        }
        else
        {
            playerSpriteRenderer.sortingOrder = -7;
            flashlight.GetComponent<SpriteRenderer>().sortingOrder = -7;
            flashlight.transform.Rotate(0.0f, 0.0f, -90.0f);
            flashlight.transform.position = playerTransform.position;
            player.previousState = States.tryingToHide;
            player.SwitchState(States.hiding, hideoutCollider);
            isHiding = false;
        }
    }


    public override void OnCollisionEnter(PlayerStateMachine player, Collision2D collision)
    {
        if (collision.collider.tag == "Monster")
        {
            inputManager.movementInputEnabled = true;
            player.SwitchState(States.dead);
        }
    }
    public override void OnTriggerStay(PlayerStateMachine player, Collider2D collision)
    {

    }
}
