using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool isLocked;
    public bool isOpened;

    [SerializeField] private int ItemIDtoUnlock;
    private Collider2D doorCollider;
    private void Awake()
    {
        doorCollider = GetComponent<Collider2D>();
    }
    public void DoorInteraction(List<int> itemIDs)
    {
        if(isLocked)
        {

        }
    }
}
