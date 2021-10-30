using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateIdle : PlayerStateBase
{
    public PlayerStateIdle(GameObject playerObject) : base(playerObject) 
    {
        rb = playerObject.GetComponent<Rigidbody2D>();
        playerInput = playerObject.GetComponent<PlayerInput>();
        inputManager = playerObject.GetComponent<InputManager>();
        playerTransform = playerObject.GetComponent<Transform>();
        flashlight = playerObject.transform.GetChild(1).gameObject;
    }

    public event EventHandler OnEnterStateIdle;
    public override void EnterState(PlayerStateMachine player)
    {
        OnEnterStateIdle?.Invoke(this, EventArgs.Empty);
        isFacingRight = player.movingState.isFacingRight;
        //Debug.Log("Hello from idle state");
    }
    public override void UpdateState(PlayerStateMachine player)
    {
        rb.velocity = new Vector2(0,0);
        if(inputManager.movementInputDirection != 0)
        {
            player.SwitchState(player.movingState);
        }
        Flashlight();

    }
    public override void OnCollisionEnter(PlayerStateMachine player, Collision2D collision)
    {
        
    }
    public override void OnTriggerStay(PlayerStateMachine player, Collider2D collision)
    {
        if (inputManager.isInteractionButtonClicked)
        {
            if (collision.CompareTag("Key"))
            {
                inputManager.isInteractionButtonClicked = false;
                collision.gameObject.SetActive(false);
                //collisio.gameObject get component jakiœ skrypt w którym bêdzie id przedmiotu
            }
            else if (collision.CompareTag("Hideout"))
            {
                inputManager.isInteractionButtonClicked = false;
                player.SwitchState(player.hidingState);
            }
        }
    }
    public void Flashlight()
    {
        if (inputManager.isFlashlightButtonClicked)
        {
            flashlight.SetActive(true);
        }
        else if (!inputManager.isFlashlightButtonClicked)
        {
            flashlight.SetActive(false);
        }
    }
}
