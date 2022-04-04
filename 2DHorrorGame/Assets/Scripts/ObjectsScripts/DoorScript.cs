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
    public AnimationClip doorOpeningAnimation;

    public float interactionTime;
    public float unlockTimeRequired;
    public int itemIdToUnlock;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private Transform leftPoint;

    private Collider2D doorCollider;
     
    private void Awake()
    {
        doorCollider = gameObject.transform.GetChild(0).GetComponent<Collider2D>();
        if (!isLocked) isOpened = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            interactionTime = 0;
        }
    }
    public void DoorUnlock(IPickableObject[] items, bool isInteractionButtonHeld)
    {
        if (isInteractionButtonHeld)
        {
            foreach (IPickableObject item in items)
            {
                if (item != null && item.ItemID == itemIdToUnlock)
                {


                    interactionTime += Time.deltaTime;
                    if (interactionTime >= unlockTimeRequired)
                    {
                        isLocked = false;
                    }
                    
                }
            }
        }
        else
        {
            interactionTime = 0;
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
}
