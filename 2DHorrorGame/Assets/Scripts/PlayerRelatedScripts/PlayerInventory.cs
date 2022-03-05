using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class PlayerInventory : MonoBehaviour
{
    private int inventorySlotCount = 3;
    private int currentInventoryItemCount;
    public List<int> inventoryItemsIDs;
    public Image[] inventorySlots;
    public Image[] inventoryItems;
    public Sprite itemContainer;
    public void Awake()
    {
        currentInventoryItemCount = 0;
        inventoryItemsIDs = new List<int>();
        for(int i = 0; i< inventorySlotCount; i++)
        {
            inventorySlots[i].sprite = itemContainer;
        }
        foreach(Image image in inventoryItems)
        {
            image.enabled = false;
        }

    }

    public bool AddItemToInventory(IPickableObject item)
    {
        if (currentInventoryItemCount < inventorySlotCount)
        {
            inventoryItems[currentInventoryItemCount].sprite = item.InventoryIcon;
            inventoryItems[currentInventoryItemCount].enabled = true;
            inventoryItemsIDs.Add(item.ItemID);
            return true;
        }
        else
        {
            //send info to UI probably idk
            return false;
        }
        
        
    }
    public void DebugLogInventory()
    {
        foreach(int i in inventoryItemsIDs)
        {
            Debug.Log(i.ToString() + ", ");
        }

    }
}
