using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool isLocked;
    public bool isOpened;

    public float unlockTimeRequired;
    [SerializeField] private int itemIdToUnlock;
    private Collider2D doorCollider;
     
    private void Awake()
    {
        doorCollider = gameObject.transform.GetChild(0).GetComponent<Collider2D>();
        if (!isLocked) isOpened = false;
    }
    public void DoorUnlock(List<int> itemIDs)
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
    public void DoorOpenOrClose()
    {
        isOpened = !isOpened;
        doorCollider.enabled = !doorCollider.enabled;
    }
}
