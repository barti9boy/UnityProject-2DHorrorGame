using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour, IInteractible
{
    public bool isLocked;
    public bool isOpened;
    public bool isChangingRoom = false;
    public int velocityDirection;
    public AnimationClip doorOpeningAnimation;
    public GameObject interactionHighlight;

    public float interactionTime;
    public float unlockTimeRequired;
    public int itemIdToUnlock;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private Transform leftPoint;
    private Transform unlockingCanvasImageTransform;

    private Collider2D doorCollider;
     
    private void Awake()
    {
        doorCollider = gameObject.transform.GetChild(0).GetComponent<Collider2D>();
        if (!isLocked) isOpened = false;
        unlockingCanvasImageTransform = gameObject.transform.GetChild(3).GetChild(0);
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
    public void ChangeInteractionCanvasTransform(float playerX, float doorX)
    {

        if(playerX > doorX)
        {
            unlockingCanvasImageTransform.localPosition = new Vector3(1, 0, 0);
        }
        else
        {
            unlockingCanvasImageTransform.localPosition = new Vector3(-1, 0, 0);
        }
    }

    public void EnableInteractionHighlight()
    {
        interactionHighlight.SetActive(true);
    }

    public void DisableInteractionHighlight()
    {
        interactionHighlight.SetActive(false);
    }
}
