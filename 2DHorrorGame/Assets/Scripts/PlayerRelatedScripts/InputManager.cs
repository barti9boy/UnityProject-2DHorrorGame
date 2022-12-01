using System;
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
    private bool isInventory1ButtonClicked;
    public float InteractionTime { get; private set; }

    public static Action<int> OnInventoryButtonClicked;


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

    public void FlashlightButton()
    {
        isFlashlightButtonClicked = !isFlashlightButtonClicked;
    }
    public void Interaction(InputAction.CallbackContext context)
    {
        Debug.Log("Interaction performed");
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

    public void Inventory1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnInventoryButtonClicked?.Invoke(0);
            Debug.Log("Item1 dropped");
        }
    }

    public void Inventory2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnInventoryButtonClicked?.Invoke(1);
            Debug.Log("Item2 dropped");
        }
    }

    public void Inventory3(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnInventoryButtonClicked?.Invoke(2);
            Debug.Log("Item3 dropped");
        }
    }
    IEnumerator ClickDuration(float s)
    {
        yield return new WaitForSeconds(s);
        isInteractionButtonClicked = false;
    }
}
