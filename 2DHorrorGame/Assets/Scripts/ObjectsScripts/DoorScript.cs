using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool isLocked;
    public bool isOpened;

    public float interactionTime;
    public float unlockTimeRequired;
    [SerializeField] private int itemIdToUnlock;
    private Collider2D doorCollider;
     
    private void Awake()
    {
        doorCollider = gameObject.transform.GetChild(0).GetComponent<Collider2D>();
        if (!isLocked) isOpened = false;
    }
    private void Update()
    {
        
    }
    public void DoorUnlock(List<int> itemIDs, bool isInteractionButtonHeld)
    {
        foreach(int ID in itemIDs)
        {
            if(ID == itemIdToUnlock)
            {
                if (isInteractionButtonHeld)
                {
                    interactionTime += Time.deltaTime;
                    if(interactionTime >= unlockTimeRequired)
                    {
                        isOpened = true;
                        isLocked = false;
                        doorCollider.enabled = false;
                    }
                }
                else
                {
                    interactionTime = 0;
                } 
            }
        }
    }
    public void DoorOpenOrClose()
    {
        isOpened = !isOpened;
        doorCollider.enabled = !doorCollider.enabled;
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
