using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class VentEntranceScript : MonoBehaviour
{
    public bool isInVent = false;
    public bool isChangingRoom = false;
    public bool isMovingDown = false;
    public bool isMovingUp = false;
    public int velocityDirection;

    private Rigidbody2D playerRb;
    private Transform playerTransform;
    private InputManager playerInputManager;

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
    }

    public void ChangeRoom(Transform transform, Rigidbody2D rb, InputManager inputManager, PlayerStateMachine player)
    {
        playerRb = rb;
        playerTransform = transform;
        playerInputManager = inputManager;
        playerInputManager.movementInputEnabled = false;
        playerInputManager.interactionInputEnabled = false;
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
            Debug.Log(playerRb.velocity.x);
            if (Math.Abs(playerTransform.position.x - gameObject.transform.position.x) < 0.1)
            {
                isChangingRoom = false;
                isMovingDown = true;
            }
        }
        if (velocityDirection == -1 && playerTransform.position.x > gameObject.transform.position.x)
        {
            playerRb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
            if (Math.Abs(playerTransform.position.x - gameObject.transform.position.x) < 0.1)
            {
                isChangingRoom = false;
                isMovingDown = true;
            }
        }
    }
    public void MovePlayerDown()
    {

    }
    public void MovePlayerUp()
    {

    }
}
