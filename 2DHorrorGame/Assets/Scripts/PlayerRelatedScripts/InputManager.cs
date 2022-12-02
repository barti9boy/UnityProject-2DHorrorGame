using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class InputManager : MonoBehaviour
{
    public float movementInputDirection;
    public bool isFlashlightButtonClicked;
    public bool isInteractionButtonClicked;
    public bool isInteractionButtonHeld;
    public bool movementInputEnabled = true;
    public bool interactionInputEnabled = true;
    public float InteractionTime { get; private set; }

    private PhotonView view;

    private void Awake()
    {
        InteractionTime = 0f;
    }
    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if(view.IsMine)
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
    }
    public void Movement(InputAction.CallbackContext context) 
    {
        if (view.IsMine)
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
    }
    public void Flashlight(InputAction.CallbackContext context) 
    {
        if(view.IsMine)
        {
            if (context.started)
            {
                isFlashlightButtonClicked = !isFlashlightButtonClicked;
            }
        }
    }

    public void FlashlightButton()
    {
        isFlashlightButtonClicked = !isFlashlightButtonClicked;
    }
    public void Interaction(InputAction.CallbackContext context)
    {
        if(view.IsMine)
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
    }
    IEnumerator ClickDuration(float s)
    {
        yield return new WaitForSeconds(s);
        isInteractionButtonClicked = false;
    }
}
