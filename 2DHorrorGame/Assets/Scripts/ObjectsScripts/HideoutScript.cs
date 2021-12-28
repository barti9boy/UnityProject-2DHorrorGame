using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class HideoutScript : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    PlayerStateMachine playerStateMachine;
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private InputManager inputManager;
    private Transform playerTransform;
    private GameObject flashlight;
    private GameObject playerGFX;
    private SpriteRenderer playerSpriteRenderer;
    private Collider2D collider;
    private Animator animator;
    [SerializeField] private Transform handle;
    [SerializeField] private AnimationClip hiding;


    public bool isHiding = false;
    public bool isHidden = false;
    public bool isTryingToHide = false;
    public int velocityDirection;
    [SerializeField] private float movementSpeed = 5.0f;

    public event EventHandler OnEnterStateHiding;
    public event EventHandler OnLeaveStateHiding;

    void Awake()
    {
        rb = playerObject.GetComponent<Rigidbody2D>();
        playerInput = playerObject.GetComponent<PlayerInput>();
        inputManager = playerObject.GetComponent<InputManager>();
        playerTransform = playerObject.GetComponent<Transform>();
        flashlight = playerObject.transform.GetChild(1).gameObject;
        playerGFX = playerObject.transform.GetChild(0).gameObject;
        playerSpriteRenderer = playerGFX.GetComponent<SpriteRenderer>();
        playerStateMachine = playerObject.GetComponent<PlayerStateMachine>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (isTryingToHide)
        {
            MovePlayer();
        }
    }

    public void CheckHideout(PlayerStateMachine player)
    {
        inputManager.movementInputEnabled = false;
        inputManager.movementInputDirection = 0;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);

        if (isHidden == false)
        {
            StartHiding(player);
        }
        else if (isHidden == true)
        {
            StartLeaving(player);
        }
    }

    public void StartHiding(PlayerStateMachine player)
    {
        animator.SetBool("isHiding", true);
        if (handle.transform.position.x > playerTransform.position.x) //jesteœmy po lewej
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
        else if (handle.transform.position.x < playerTransform.position.x) // jesteœmy po prawej
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
        isTryingToHide = true;
    }


    public void Hide()
    {
        playerSpriteRenderer.sortingOrder = -7;
        flashlight.GetComponent<SpriteRenderer>().sortingOrder = -7;
        flashlight.transform.Rotate(0.0f, 0.0f, -90.0f);
        flashlight.transform.position = playerTransform.position;
        playerStateMachine.SwitchState(playerStateMachine.hidingState);
        animator.SetBool("isHiding", false);
        animator.SetBool("isHidden", true);
        isHidden = true;

    }
    public void MovePlayer()
    {
        if (velocityDirection == 1 && playerTransform.position.x < handle.position.x)
        {
            rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
            if (Math.Abs(playerTransform.position.x - handle.transform.position.x) < 0.1)
            {
                isTryingToHide = false;
                inputManager.interactionInputEnabled = true;
                OnEnterStateHiding?.Invoke(this, EventArgs.Empty);
                rb.velocity = new Vector2(0, 0);
                StartCoroutine(WaitForHidingTime());
              
            }
        }
        if (velocityDirection == -1 && playerTransform.position.x > handle.position.x)
        {
            rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
            if (Math.Abs(playerTransform.position.x - handle.transform.position.x) < 0.1)
            {
                isTryingToHide = false;
                inputManager.interactionInputEnabled = true;
                OnEnterStateHiding?.Invoke(this, EventArgs.Empty);
                rb.velocity = new Vector2(0, 0);
                StartCoroutine(WaitForHidingTime());
            }
        }
    }

    private IEnumerator WaitForHidingTime()
    {
        yield return new WaitForSeconds(hiding.length);
        Hide();
    }


    private IEnumerator WaitForLeavingTime()
    {
        yield return new WaitForSeconds(hiding.length);
        Leave();
    }

    public void StartLeaving(PlayerStateMachine player)
    {
        animator.SetBool("isHidden", false);
        animator.SetBool("isLeaving", true);
        playerSpriteRenderer.sortingOrder = 1;
        flashlight.GetComponent<SpriteRenderer>().sortingOrder = 1;
        OnLeaveStateHiding?.Invoke(this, EventArgs.Empty);
        StartCoroutine(WaitForLeavingTime());



        /* inputManager.isInteractionButtonClicked = false;
         flashlight.transform.Rotate(0.0f, 0.0f, 90.0f);
         if (player.currentState.isFacingRight)
         {
             flashlight.transform.position = new Vector3(playerTransform.position.x + 0.2f, playerTransform.position.y, playerTransform.position.z);
         }
         else if (!player.currentState.isFacingRight)
         {
             flashlight.transform.position = new Vector3(playerTransform.position.x - 0.2f, playerTransform.position.y, playerTransform.position.z);
         }
         playerSpriteRenderer.sortingOrder = 1;
         flashlight.GetComponent<SpriteRenderer>().sortingOrder = 1;
         inputManager.movementInputEnabled = true;
         player.SwitchState(player.idleState);
         isHidden = false;
         animator.SetBool("isLeaving", false); */
    }

    public void Leave()
    {
        inputManager.isInteractionButtonClicked = false;
        flashlight.transform.Rotate(0.0f, 0.0f, 90.0f);
        if (playerStateMachine.currentState.isFacingRight)
        {
            flashlight.transform.position = new Vector3(playerTransform.position.x + 0.2f, playerTransform.position.y, playerTransform.position.z);
        }
        else if (!playerStateMachine.currentState.isFacingRight)
        {
            flashlight.transform.position = new Vector3(playerTransform.position.x - 0.2f, playerTransform.position.y, playerTransform.position.z);
        }
        inputManager.movementInputEnabled = true;
        playerStateMachine.SwitchState(playerStateMachine.idleState);
        isHidden = false;
        animator.SetBool("isLeaving", false);
    }
}
