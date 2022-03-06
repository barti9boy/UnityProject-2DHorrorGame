using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class PlayerInventory : MonoBehaviour
{
    public IPickableObject[] items;
    public Image[] inventorySlots;
    public Image[] inventoryItems;
    public Sprite itemContainer;

    private int inventorySlotCount = 3;
    private int currentInventoryItemCount;
    private InventoryItemScript inventoryItemScript;

    public void Awake()
    {
        currentInventoryItemCount = 0;
        items = new IPickableObject[inventorySlotCount];
        for(int i = 0; i< inventorySlotCount; i++)
        {
            inventorySlots[i].sprite = itemContainer;
        }
        foreach(Image image in inventoryItems)
        {
            image.enabled = false;
            inventoryItemScript = image.GetComponent<InventoryItemScript>();
            inventoryItemScript.OnItemDrop += OnRemoveItemFromInventory;
        }
    }

    public bool AddItemToInventory(IPickableObject item)
    {
        if (currentInventoryItemCount < inventorySlotCount)
        {
            inventoryItems[currentInventoryItemCount].sprite = item.InventoryIcon;
            inventoryItems[currentInventoryItemCount].enabled = true;
            inventoryItems[currentInventoryItemCount].GetComponent<InventoryItemScript>().isEmpty = false;
            items[currentInventoryItemCount] = item;
            Debug.Log(items[currentInventoryItemCount].ItemID);
            currentInventoryItemCount++;
            return true;
        }
        else
        {
            //send info to UI probably idk
            return false;
        }
    }

    public void OnRemoveItemFromInventory(object sender, EventArgs e)
    {
        for(int slotNumber = 0; slotNumber <inventorySlotCount; slotNumber++)
        {
            if (inventoryItems[slotNumber].GetComponent<InventoryItemScript>().isEmpty == true)
            {
                inventoryItems[slotNumber].sprite = null;
                inventoryItems[slotNumber].enabled = false;
                if (currentInventoryItemCount > 0) currentInventoryItemCount--;
                items[slotNumber].ChangePosition(gameObject.transform.position.x, gameObject.transform.position.y - 1.07f);
                return;
            }
        }

    }


    public void DebugLogInventory()
    {
        foreach(IPickableObject i in items)
        {
            Debug.Log(i.ItemID.ToString() + ", ");
        }

    }
}
