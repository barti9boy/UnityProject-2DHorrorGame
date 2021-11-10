using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public float movementInputDirection;
    public bool isFlashlightButtonClicked;
    public bool isInteractionButtonClicked;
    public bool isInteractionButtonHeld;
    public bool movementInputEnabled = true;
    public bool interactionInputEnabled = true;
    public float InteractionTime { get; private set; }

    private void Awake()
    {
        InteractionTime = 0f;
    }
    private void Update()
    {
        if(isInteractionButtonHeld && interactionInputEnabled)
        {
            InteractionTime += Time.deltaTime;
        }
        else
        {
            InteractionTime = 0;
        }
    }
    public void Movement(InputAction.CallbackContext context) 
    {
        if (!movementInputEnabled)
        {
            movementInputDirection = 0;
        }
        else
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
        if(context.performed && interactionInputEnabled)
        {
            isInteractionButtonClicked = true;
            StartCoroutine(ClickDuration(0.1f));
            //inspector sometimes does not register bool change and does not tick the box, but this works correctly
            
        }
        if(context.performed && interactionInputEnabled)
        {
            isInteractionButtonHeld = true;
        }
        if (context.canceled)
        {
            isInteractionButtonHeld = false;
        }
    }
    IEnumerator ClickDuration(float s)
    {
        yield return new WaitForSeconds(s);
        isInteractionButtonClicked = false;
    }
}
