using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool isLocked;
    public bool isOpened;

    [SerializeField] private int itemIDtoUnlock;
    private Collider2D doorCollider;
    private void Awake()
    {
        doorCollider = GetComponent<Collider2D>();
    }
    public void DoorInteraction(List<int> itemIDs)
    {
        if(isLocked)
        {
            foreach(int ID in itemIDs)
            {
                if(ID == itemIDtoUnlock)
                {
                    isLocked = false;
                    isOpened = true;
                }
            }
        }
    }
}
