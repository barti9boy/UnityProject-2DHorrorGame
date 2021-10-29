using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool isLocked;
    public bool isOpened;

    [SerializeField] private int itemIdToUnlock;
    [SerializeField] private float unlockingTimeRequired;
    private Collider2D doorCollider;
     
    private void Awake()
    {
        doorCollider = gameObject.transform.GetChild(0).GetComponent<Collider2D>();
        if (!isLocked) isOpened = false;
    }
    public void DoorInteraction(List<int> itemIDs)
    {
        if(isLocked)
        {
            foreach(int ID in itemIDs)
            {
                if(ID == itemIdToUnlock)
                {
                        isOpened = true;
                        isLocked = false;
                        doorCollider.enabled = false;
                }
            }
        }
        else
        {
            isOpened = !isOpened;
            doorCollider.enabled = !doorCollider.enabled;
        }
    }
}
