using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public static class PlayerActions
{
    public static void Flip(PlayerStateMachine player)
    {
        if (player.currentState.inputManager.movementInputDirection == 1 && !player.isFacingRight)
        {
            player.transform.Rotate(0.0f, 180.0f, 0.0f);
            player.isFacingRight = true;
        }
        else if (player.currentState.inputManager.movementInputDirection == -1 && player.isFacingRight)
        {
            player.transform.Rotate(0.0f, 180.0f, 0.0f);
            player.isFacingRight = false;
        }
    }

    public static void Flashlight(PlayerStateMachine player)
    {
        if (player.currentState.inputManager.isFlashlightButtonClicked && !player.flashlightOutOfBattery)
        {
            player.currentState.flashlight.SetActive(true);
        }
        else if (!player.currentState.inputManager.isFlashlightButtonClicked)
        {
            player.currentState.flashlight.SetActive(false);
        }
    }
    public static void Interact(PlayerStateMachine player, Collider2D collision)
    {
        if (player.currentState.inputManager.isInteractionButtonClicked)
        {
            //if (collision.CompareTag("Key"))
            //{
            //    player.SwitchState(player.itemPickupState);
            //    if (player.currentState.playerInventory.AddItemToInventory(collision.gameObject.GetComponent<IPickableObject>()))
            //    {
            //        collision.gameObject.SetActive(false);
            //    }
            //    player.currentState.inputManager.isInteractionButtonClicked = false;
                
            //}
            if (collision.CompareTag("Item") || collision.CompareTag("Battery"))
            {
                player.SwitchState(States.itemPickup, collision);
                player.currentState.inputManager.isInteractionButtonClicked = false;
                //player.currentState.playerInventory.DebugLogInventory();
            }

            if (collision.CompareTag("Hideout") || collision.CompareTag("Closet") || collision.CompareTag("Table"))
            {
                player.currentState.inputManager.movementInputEnabled = false;
                player.previousState = States.idle;
                player.currentState.inputManager.isInteractionButtonClicked = false;
                player.SwitchState(States.tryingToHide, collision);
            }
            if (collision.CompareTag("Switch"))
            {
                //collision.gameObject.GetComponent<CameraSwitchVertical>().ChangeRoom(player.currentState.playerTransform, player.currentState.rb, player.currentState.inputManager, player);
                collision.gameObject.GetComponent<CameraSwitchVertical>().ChangeCamera();
            }
        }
        if (collision.CompareTag("Doors"))
        {
            if (collision.gameObject.GetComponent<DoorScript>().isLocked)
            {
                collision.gameObject.GetComponent<DoorScript>().ChangeInteractionCanvasTransform(player.transform.position.x, collision.transform.position.x);
                collision.gameObject.GetComponent<DoorScript>().DoorUnlock(player.currentState.playerInventory.items, player.currentState.inputManager.isInteractionButtonHeld);
            }
            else
            {
                if (player.currentState.inputManager.isInteractionButtonClicked)
                {
                    collision.GetComponent<IInteractible>().DisableInteractionHighlight();
                    player.currentState.inputManager.isInteractionButtonClicked = false;
                    player.previousState = States.idle;
                    player.SwitchState(States.usingHorizontalDoor, collision);
                }
            }
        }
        if (collision.CompareTag("vDoors"))
        {
            if (collision.gameObject.GetComponent<DoorScript>().isLocked)
            {
                collision.gameObject.GetComponent<DoorScript>().ChangeInteractionCanvasTransform(player.transform.position.x, collision.transform.position.x);
                collision.gameObject.GetComponent<DoorScript>().DoorUnlock(player.currentState.playerInventory.items, player.currentState.inputManager.isInteractionButtonHeld);
            }
            else 
            { 
                if (player.currentState.inputManager.isInteractionButtonClicked)
                {
                    collision.GetComponent<IInteractible>().DisableInteractionHighlight();
                    player.currentState.inputManager.isInteractionButtonClicked = false;
                    player.previousState = States.idle;
                    player.SwitchState(States.usingVerticalDoor, collision);
                }
            }
        }
            if (player.currentState.inputManager.isInteractionButtonClicked)
        {
            if (collision.CompareTag("Ladder"))
            {
                player.currentState.inputManager.isInteractionButtonClicked = false;
                player.previousState = States.idle;
                player.SwitchState(States.usingLadder, collision);
            }
        }
    }
    public static void Hiding(PlayerStateMachine player, Collider2D collision, Transform playerTransform, float movementSpeed, Rigidbody2D rb, Collision2D collider)
    {
        if (playerTransform.position.x != collision.transform.position.x)
        {
            if (playerTransform.position.x - collision.transform.position.x < 0)
            {
                rb.velocity = new Vector2(movementSpeed * 1 / 2, 0);
            }
            else if (playerTransform.position.x - collision.transform.position.x > 0)
            {
                rb.velocity = new Vector2(movementSpeed * -1 / 2, 0);
            }
        }
        if (Math.Abs(playerTransform.position.x - collision.transform.position.x) < 0.1)
        {
            collider = null;
        }

    }



}
