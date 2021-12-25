using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class LadderScript : MonoBehaviour
{
    public bool isFirstTimeGoingUp = true;
    public bool isFirstTimeGoingDown = true;
    public bool isChangingRoom = false;
    public bool isMovingDown = false;
    public bool isMovingUp = false;
    public int velocityDirection;

    private Rigidbody2D playerRb;
    private Transform playerTransform;
    private InputManager playerInputManager;
    private GameObject playerFlashlight;
    private PlayerStateMachine playerStateMachine;

    [SerializeField] private bool isEntrance;
    [SerializeField] private Transform DownPoint;
    [SerializeField] private Transform UpPoint;
    [SerializeField] private float movementSpeed = 5.0f;

    public event EventHandler OnLadderMoveUp;
    public event EventHandler OnLadderMoveDown;

    void Update()
    {
        if(isChangingRoom)
        {
            MovePlayer();
        }
        if(isMovingDown)
        {
            MovePlayerDown();
        }
        if (isMovingUp)
        {
            MovePlayerUp();
        }
    }

    public void ChangeRoom(Transform transform, Rigidbody2D rb, InputManager inputManager, PlayerStateMachine player, GameObject flashlight)
    {
        playerStateMachine = player;
        playerRb = rb;
        playerTransform = transform;
        playerInputManager = inputManager;
        playerFlashlight = flashlight;
        playerInputManager.movementInputEnabled = false;
        playerInputManager.interactionInputEnabled = false;
        if (isFirstTimeGoingUp && playerTransform.position.y > transform.position.y) //without isFirstTimeEntering the point moves slightly down
        {
            UpPoint.transform.position = new Vector2(UpPoint.transform.position.x, playerTransform.position.y);
            isFirstTimeGoingUp = false;
        }
        if(isFirstTimeGoingDown && playerTransform.position.y < transform.position.y)
        {
            DownPoint.transform.position = new Vector2(DownPoint.transform.position.x, playerTransform.position.y);
            isFirstTimeGoingDown = false;
        }

        if (playerTransform.position.x < gameObject.transform.position.x) 
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
        else if (playerTransform.position.x > gameObject.transform.position.x)
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
        isChangingRoom = true;
        playerFlashlight.transform.Rotate(0.0f, 0.0f, -90.0f);
    }
    public void MovePlayer()
    {
        if (velocityDirection == 1 && playerTransform.position.x < gameObject.transform.position.x)
        {
            playerRb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
            if (Math.Abs(playerTransform.position.x - gameObject.transform.position.x) < 0.1)
            {
                isChangingRoom = false;
                if (playerTransform.position.y > transform.position.y) isMovingDown = true;
                if (playerTransform.position.y < transform.position.y) isMovingUp = true;
            }
        }
        if (velocityDirection == -1 && playerTransform.position.x > gameObject.transform.position.x)
        {
            playerRb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
            if (Math.Abs(playerTransform.position.x - gameObject.transform.position.x) < 0.1)
            {
                isChangingRoom = false;
                if (playerTransform.position.y > transform.position.y) isMovingDown = true;
                if (playerTransform.position.y < transform.position.y) isMovingUp = true;
            }
        }
    }
    public void MovePlayerDown()
    {
        OnLadderMoveDown?.Invoke(this, EventArgs.Empty);
        if (playerTransform.position.y > DownPoint.position.y)
        {
            playerRb.velocity = new Vector2(0, -1f * movementSpeed);
        }
        if(playerTransform.position.y < DownPoint.position.y)
        {
            playerTransform.position = new Vector2(playerTransform.position.x, DownPoint.position.y);
            isMovingDown = false;
            playerInputManager.movementInputEnabled = true;
            playerInputManager.interactionInputEnabled = true;
            playerFlashlight.transform.Rotate(0.0f, 0.0f, 90.0f);
        }
        if (isEntrance)
        {
            playerStateMachine.currentState.isInVent = !playerStateMachine.currentState.isInVent;
        }
    }
    public void MovePlayerUp()
    {
        OnLadderMoveUp?.Invoke(this, EventArgs.Empty);
        if (playerTransform.position.y < UpPoint.position.y)
        {
            playerRb.velocity = new Vector2(0, 1f * movementSpeed);
        }
        if (playerTransform.position.y > UpPoint.position.y)
        {
            playerTransform.position = new Vector2(playerTransform.position.x, UpPoint.position.y);
            isMovingUp = false;
            playerInputManager.movementInputEnabled = true;
            playerInputManager.interactionInputEnabled = true;
            playerFlashlight.transform.Rotate(0.0f, 0.0f, 90.0f);
        }
        if (isEntrance)
        {
            playerStateMachine.currentState.isInVent = !playerStateMachine.currentState.isInVent;
        }
    }
}
