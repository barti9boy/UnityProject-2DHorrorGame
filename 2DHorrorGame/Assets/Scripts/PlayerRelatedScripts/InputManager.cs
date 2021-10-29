using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movementInputDirection;
    public bool isFlashlightButtonClicked;
    public bool isInteractionButtonClicked;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }
    public void Movement(InputAction.CallbackContext context) 
    {
        if(context.performed)
        {
            //Debug.Log("" + context.ReadValue<float>());
            movementInputDirection = context.ReadValue<float>();
        }
        else
        {
            movementInputDirection = 0;
        }
    }
    public void Flashlight(InputAction.CallbackContext context) 
    {
        if (context.started)
        {
            isFlashlightButtonClicked = !isFlashlightButtonClicked;
        }
    }
    public void Interaction(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            isInteractionButtonClicked = true;
        }
        if(context.canceled)
        {
            isInteractionButtonClicked = false;
        }
    }
}
