using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class VentEntranceScript : MonoBehaviour
{
    public bool isChangingRoom = false;
    public int velocityDirection;

    private Rigidbody2D playerRb;
    private Transform playerTransform;
    private InputManager playerInputManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TransportToEntranceCenter(Transform transform, Rigidbody2D rb, InputManager inputManager, PlayerStateMachine player)
    {
        playerRb = rb;
        playerTransform = transform;
        playerInputManager = inputManager;
        playerInputManager.movementInputEnabled = false;
        playerInputManager.interactionInputEnabled = false;
        if (Math.Abs(rightPoint.transform.position.x - playerTransform.position.x) > Math.Abs(leftPoint.transform.position.x - playerTransform.position.x)) //jesteœmy po lewej
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
}
    public void TransportDown(Transform transform, Rigidbody2D rb, InputManager inputManager, PlayerStateMachine player)
    {

    }
    public void TransportUp(Transform transform, Rigidbody2D rb, InputManager inputManager, PlayerStateMachine player)
    {

    }
}
