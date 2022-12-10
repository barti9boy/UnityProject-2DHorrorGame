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
    public int PlayerBatteries { get; private set; } = 0;

    public event EventHandler<ItemEventArgs> OnItemAdd;
    public event EventHandler<string> OnItemSendNotification;

    public void Awake()
    {

    }
    private void Start()
    {
        args = new ItemEventArgs();
        items = new IPickableObject[inventorySlotCount];
        for (int slotNumber = 0; slotNumber < inventorySlotCount; slotNumber++)
        {
            inventoryItems[slotNumber].enabled = false;
            inventoryItems[slotNumber].GetComponent<InventoryItemScript>().slotNumber = slotNumber;
        }
        InventoryItemScript.OnItemDrop += RemoveItemFromInventory;
        InputManager.OnInventoryButtonClicked += RemoveItemFromInventory;
        DoorScript.OnDoorUnlocked += DestroyItemFromInventory;
    }

    public void OnDestroy()
    {
        InventoryItemScript.OnItemDrop -= RemoveItemFromInventory;
        InputManager.OnInventoryButtonClicked -= RemoveItemFromInventory;
        DoorScript.OnDoorUnlocked -= DestroyItemFromInventory;
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
    public void AddBateryToInventory()
    {
        PlayerBatteries++;
        //Update Batteries UI
    }
    public void TryChangeBattery()
    {
        int i = 0;
        foreach (var item in items)
        {
            if(item != null)
            {
                if (item.DisplayName == "Battery")
                {
                    DestroyItemFromInventory(i);
                    PlayerBatteries--;
                }
                i++;
            }
        }
        //Update Batteries UI
    }
    public void RemoveItemFromInventory(int slotNumber)
    {
        if(items[slotNumber] != null)
        {
            if(items[slotNumber].DisplayName == "Battery")
                PlayerBatteries--;

            items[slotNumber].ChangePosition(this.gameObject.transform.position.x, this.transform.position.y - 1.07f); //1.07 is the distance from playerObject to floor
            items[slotNumber] = null;
        }
    }

    public void DestroyItemFromInventory(int slotNumber)
    {
        items[slotNumber].DestroyItem();
    }





    public void DebugLogInventory()
    {
        foreach(IPickableObject i in items)
        {
            Debug.Log(i.ItemID.ToString() + ", ");
        }
    }
}
