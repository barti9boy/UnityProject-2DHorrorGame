using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class HideoutScript : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private InputManager inputManager;
    private Transform playerTransform;
    private GameObject flashlight;
    private GameObject playerGFX;
    private SpriteRenderer playerSpriteRenderer;
    private Collider2D collider;
    [SerializeField] private Transform handle;


    public bool isHiding = false;
    public bool isTryingToHide = false;
    public int velocityDirection;
    [SerializeField] private float movementSpeed = 5.0f;



    // Start is called before the first frame update
    void Awake()
    {
        rb = playerObject.GetComponent<Rigidbody2D>();
        playerInput = playerObject.GetComponent<PlayerInput>();
        inputManager = playerObject.GetComponent<InputManager>();
        playerTransform = playerObject.GetComponent<Transform>();
        flashlight = playerObject.transform.GetChild(1).gameObject;
        playerGFX = playerObject.transform.GetChild(0).gameObject;
        playerSpriteRenderer = playerGFX.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTryingToHide)
        {
            MovePlayer();
        }
    }

    public void StartHiding(PlayerStateMachine player)
    {
        if (handle.transform.position.x > playerTransform.position.x) //jesteœmy po lewej
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
        else if (handle.transform.position.x < playerTransform.position.x) // jesteœmy po prawej
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
        isTryingToHide = true;
    }


    public void Hide()
    {
        playerSpriteRenderer.sortingOrder = -7;
        flashlight.GetComponent<SpriteRenderer>().sortingOrder = -7;
        rb.velocity = new Vector2(0, 0);
        flashlight.transform.Rotate(0.0f, 0.0f, -90.0f);
        flashlight.transform.position = playerTransform.position;
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
                Hide();
            }
        }
        if (velocityDirection == -1 && playerTransform.position.x > handle.position.x)
        {
            rb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
            if (Math.Abs(playerTransform.position.x - handle.transform.position.x) < 0.1)
            {
                isTryingToHide = false;
                inputManager.interactionInputEnabled = true;
                Hide();
            }
        }
    }
}
