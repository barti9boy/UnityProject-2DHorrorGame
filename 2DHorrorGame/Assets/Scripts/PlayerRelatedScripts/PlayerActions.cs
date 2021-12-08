using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerActions
{
    public static void Flip(PlayerStateMachine player)
    {
        if (player.currentState.inputManager.movementInputDirection == 1 && !player.currentState.isFacingRight)
        {
            player.currentState.playerTransform.Rotate(0.0f, 180.0f, 0.0f);
            player.currentState.isFacingRight = true;
        }
        else if (player.currentState.inputManager.movementInputDirection == -1 && player.currentState.isFacingRight)
        {
            player.currentState.playerTransform.Rotate(0.0f, 180.0f, 0.0f);
            player.currentState.isFacingRight = false;
        }
    }
    public static void Flashlight(PlayerStateMachine player)
    {
        if (player.currentState.inputManager.isFlashlightButtonClicked)
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
            if (collision.CompareTag("Key"))
            {
                player.currentState.playerInventory.AddItemToInventory(collision.gameObject.GetComponent<KeyScript>().ItemID);
                collision.gameObject.SetActive(false);
                player.currentState.playerInventory.DebugLogInventory();
            }
            if (collision.CompareTag("Hideout"))
            {
                player.currentState.inputManager.movementInputEnabled = false;
                player.previousState = player.currentState;
                player.currentState.inputManager.isInteractionButtonClicked = false;
                collision.gameObject.GetComponent<HideoutScript>().StartHiding(player);
                player.SwitchState(player.hidingState);

            }
        }
        if (collision.CompareTag("Doors"))
        {
            if (collision.gameObject.GetComponent<DoorScript>().isLocked)
            {
                collision.gameObject.GetComponent<DoorScript>().DoorUnlock(player.currentState.playerInventory.inventoryItemsIDs, player.currentState.inputManager.isInteractionButtonHeld);
            }
            else
            {
                if (player.currentState.inputManager.isInteractionButtonClicked)
                {
                    collision.gameObject.GetComponent<DoorScript>().DoorOpen();
                    collision.gameObject.GetComponent<DoorScript>().ChangeRoom(player.currentState.playerTransform, player.currentState.rb, player.currentState.inputManager, player);
                }
            }
        }
        if (collision.CompareTag("VentEntrance"))
        {
            if (player.currentState.inputManager.isInteractionButtonClicked)
            {
                collision.gameObject.GetComponent<VentEntranceScript>().ChangeRoom(player.currentState.playerTransform, player.currentState.rb, player.currentState.inputManager, player);
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
