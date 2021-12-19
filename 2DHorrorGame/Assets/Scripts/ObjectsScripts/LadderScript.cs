using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class LadderScript : MonoBehaviour
{
    public bool isInVent = false;
    public bool isChangingRoom = false;
    public bool isMovingDown = false;
    public bool isMovingUp = false;
    public int velocityDirection;

    private Rigidbody2D playerRb;
    private Transform playerTransform;
    private InputManager playerInputManager;

    [SerializeField] private Transform DownPoint;
    [SerializeField] private Transform UpPoint;
    [SerializeField] private float movementSpeed = 5.0f;

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

    public void ChangeRoom(Transform transform, Rigidbody2D rb, InputManager inputManager, PlayerStateMachine player)
    {
        
        playerRb = rb;
        playerTransform = transform;
        playerInputManager = inputManager;
        playerInputManager.movementInputEnabled = false;
        playerInputManager.interactionInputEnabled = false;
        if (!isInVent) UpPoint.transform.position = new Vector2(UpPoint.transform.position.x, playerTransform.position.y);
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
    }
    public void MovePlayer()
    {

            if (velocityDirection == 1 && playerTransform.position.x < gameObject.transform.position.x)
            {
                playerRb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
                if (Math.Abs(playerTransform.position.x - gameObject.transform.position.x) < 0.1)
                {
                    isChangingRoom = false;
                    if (!isInVent) isMovingDown = true;
                    if (isInVent) isMovingUp = true;
                }
            }
            if (velocityDirection == -1 && playerTransform.position.x > gameObject.transform.position.x)
            {
                playerRb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
                if (Math.Abs(playerTransform.position.x - gameObject.transform.position.x) < 0.1)
                {
                    isChangingRoom = false;
                    if (!isInVent) isMovingDown = true;
                    if (isInVent) isMovingUp = true;
                }
            }
    }
    public void MovePlayerDown()
    {

        if(playerTransform.position.y > DownPoint.position.y)
        {
            playerRb.velocity = new Vector2(0, -1f * movementSpeed);
        }
        if(playerTransform.position.y < DownPoint.position.y)
        {
            playerTransform.position = new Vector2(playerTransform.position.x, DownPoint.position.y);
            isMovingDown = false;
            playerInputManager.movementInputEnabled = true;
            playerInputManager.interactionInputEnabled = true;
            isInVent = true;
        }
    }
    public void MovePlayerUp()
    {
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
            isInVent = false;
        }
    }
}
