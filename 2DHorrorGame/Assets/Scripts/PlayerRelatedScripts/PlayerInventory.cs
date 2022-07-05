using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ItemEventArgs
{
    public int inventorySlotNumber;
    public IPickableObject inventoryItem;
}


public class PlayerInventory : MonoBehaviour
{
    public IPickableObject[] items;
    public Image[] inventoryItems;

    private int inventorySlotCount = 3;
    private ItemEventArgs args;

    public event EventHandler<ItemEventArgs> OnItemAdd;
    public event EventHandler<string> OnItemSendNotification;

    public void Awake()
    {
        args = new ItemEventArgs();
        items = new IPickableObject[inventorySlotCount];
        for (int slotNumber = 0; slotNumber < inventorySlotCount; slotNumber++)
        {
            inventoryItems[slotNumber].enabled = false;
            inventoryItems[slotNumber].GetComponent<InventoryItemScript>().OnItemDrop += RemoveItemFromInventory;
            inventoryItems[slotNumber].GetComponent<InventoryItemScript>().slotNumber = slotNumber;
        }
    }

    public bool AddItemToInventory(IPickableObject item)
    {
        for (int slotNumber = 0; slotNumber < inventorySlotCount; slotNumber++)
        {
            if (items[slotNumber] == null)
            {
                items[slotNumber] = item;
                args.inventorySlotNumber = slotNumber;
                args.inventoryItem = item;
                OnItemAdd?.Invoke(this, args);
                OnItemSendNotification?.Invoke(this, "picked up " + args.inventoryItem.DisplayName);
                return true;
            }
        }
        OnItemSendNotification?.Invoke(this, "inventory full");
        return false;

    }

    public void RemoveItemFromInventory(object sender, int slotNumber)
    {
        items[slotNumber].ChangePosition(this.gameObject.transform.position.x, this.transform.position.y - 1.07f); //1.07 is the distance from playerObject to floor
        items[slotNumber] = null;
        return;

    }




    public void DebugLogInventory()
    {
        foreach(IPickableObject i in items)
        {
            Debug.Log(i.ItemID.ToString() + ", ");
        }
    }
}
