using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool isLocked;
    public bool isOpened;
    public bool isChangingRoom = false;
    public int velocityDirection;

    public float interactionTime;
    public float unlockTimeRequired;
    [SerializeField] private int itemIdToUnlock;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private float movementSpeed = 5.0f;

    private Collider2D doorCollider;
    private Rigidbody2D playerRb;
    private Transform playerTransform;
     
    private void Awake()
    {
        doorCollider = gameObject.transform.GetChild(0).GetComponent<Collider2D>();
        if (!isLocked) isOpened = false;
    }
    private void Update()
    {
        if (isChangingRoom)
        {
            MovePlayer();
        }
    }
    public void DoorUnlock(List<int> itemIDs, bool isInteractionButtonHeld)
    {
        foreach(int ID in itemIDs)
        {
            if(ID == itemIdToUnlock)
            {
                if (isInteractionButtonHeld)
                {
                    interactionTime += Time.deltaTime;
                    if(interactionTime >= unlockTimeRequired)
                    {
                        isOpened = true;
                        isLocked = false;
                        doorCollider.enabled = false;
                    }
                }
                else
                {
                    interactionTime = 0;
                } 
            }
        }
    }
    public void DoorOpen()
    {
        isOpened = true;
        doorCollider.enabled = false;
    }

    public void DoorClose()
    {
        isOpened = false;
        doorCollider.enabled = true;
    }


    public void DoorInteractionTimer(bool isInteractionButtonHeld)
    {
        if(isInteractionButtonHeld)
        {
            interactionTime += Time.deltaTime;
        }
        else
        {
            interactionTime = 0;
        }
    }

    public void ChangeRoom(Transform transform ,Rigidbody2D rb, InputManager inputManager, PlayerStateMachine player)
    {
        playerRb = rb;
        playerTransform = transform;
        if(Math.Abs(rightPoint.transform.position.x - playerTransform.position.x) > Math.Abs(leftPoint.transform.position.x - playerTransform.position.x)) //jesteœmy po lewej
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
        else if (Math.Abs(rightPoint.transform.position.x - playerTransform.position.x) < Math.Abs(leftPoint.transform.position.x - playerTransform.position.x)) // jesteœmy po prawej
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
        if(velocityDirection == 1 && playerTransform.position.x < rightPoint.position.x)
        {
            playerRb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
            if(Math.Abs(playerTransform.position.x - rightPoint.transform.position.x) < 0.1)
            {
                isChangingRoom = false;
                DoorClose();
            }
        }
        if (velocityDirection == -1 && playerTransform.position.x > leftPoint.position.x)
        {
            playerRb.velocity = new Vector2(velocityDirection * movementSpeed, 0);
            if (Math.Abs(playerTransform.position.x - leftPoint.transform.position.x) < 0.1)
            {
                isChangingRoom = false;
                DoorClose();
            }
        }
    }
}
