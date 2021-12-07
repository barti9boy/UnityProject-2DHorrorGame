using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;


public class CameraSwitchVertical : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerCamera1;
    [SerializeField] private CinemachineVirtualCamera playerCamera2;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform stopPoint;
    [SerializeField] private float movementSpeed = 5.0f;
    private bool isChangingRoom = false;

    private Rigidbody2D playerRb;
    private Transform playerTransform;
    private InputManager playerInputManager;

    private void Update()
    {
        if (isChangingRoom)
        {
            MovePlayer();
        }
    }
    public void ChangeCamera()
    {
            playerCamera1.Priority = 0;
            playerCamera2.Priority = 1;
    }
    public void ChangeRoom(Transform transform, Rigidbody2D rb, InputManager inputManager, PlayerStateMachine player)
    {
        playerRb = rb;
        playerTransform = transform;
        playerInputManager = inputManager;
        playerInputManager.movementInputEnabled = false;
        playerInputManager.interactionInputEnabled = false;
        playerTransform.position = startPoint.position;
        isChangingRoom = true;
    }

    public void MovePlayer()
    {
        if (playerTransform.position.x < stopPoint.position.x)
        {
            playerRb.velocity = new Vector2(movementSpeed, 0);
            Debug.Log("new vel");
            if (Math.Abs(playerTransform.position.x - stopPoint.position.x) < 0.1)
            {
                isChangingRoom = false;
                playerInputManager.movementInputEnabled = true;
                playerInputManager.interactionInputEnabled = true;
                Debug.Log("finished");
            }
        }
    }
}
