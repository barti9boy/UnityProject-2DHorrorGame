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
    //[SerializeField] private Rigidbody2D playerRb;
    //[SerializeField] private float movementSpeed = 5.0f;
    //[SerializeField] private bool isChangingRoom = false;
    //private float velocityDirection;
    

    
    //private Transform playerTransform;
    //private InputManager playerInputManager;

    //private void Awake()
    //{
    //    playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    //}

    //private void Update()
    //{
    //    if (isChangingRoom)
    //    {
    //        MovePlayer();
    //    }
    //}
    public void ChangeCamera()
    {
            playerCamera1.Priority = 0;
            playerCamera2.Priority = 1;
    }
//    public void ChangeRoom(Transform transform, Rigidbody2D rb, InputManager inputManager, PlayerStateMachine player)
//    {
//        playerTransform = transform;
//        playerInputManager = inputManager;
//        inputManager.isInteractionButtonClicked = false;
//        playerInputManager.movementInputEnabled = false;
//        playerInputManager.interactionInputEnabled = false;
//        playerTransform.position = startPoint.position;

//        if (startPoint.position.x < stopPoint.position.x) //jesteœmy po lewej
//        {
//            if (!player.currentState.isFacingRight)
//            {
//                playerTransform.Rotate(0, 180, 0);
//                player.currentState.isFacingRight = !player.currentState.isFacingRight;
//                Debug.Log(player.currentState.isFacingRight);
//                inputManager.isInteractionButtonClicked = false;
//            }
//            velocityDirection = 1;
//        }
//        else if (startPoint.position.x > stopPoint.position.x) // jesteœmy po prawej
//        {
//            if (player.currentState.isFacingRight)
//            {
//                playerTransform.Rotate(0, 180, 0);
//                player.currentState.isFacingRight = !player.currentState.isFacingRight;
//                Debug.Log(player.currentState.isFacingRight);
//                inputManager.isInteractionButtonClicked = false;
//            }
//            velocityDirection = -1;
//        }
//        isChangingRoom = true;
//    }

//    public void MovePlayer()
//    {
//        playerRb.velocity = new Vector2(movementSpeed * velocityDirection, 0);
//        Debug.Log("new vel");
//        if (Math.Abs(playerTransform.position.x - stopPoint.position.x) < 0.1)
//        {
//             isChangingRoom = false;
//             playerRb.velocity = new Vector2(0, 0);
//             playerInputManager.movementInputEnabled = true;
//             playerInputManager.interactionInputEnabled = true;
//             Debug.Log("finished");
//        }
//    }
}
