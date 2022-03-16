using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;


public class PlayerInventory : MonoBehaviour
{
    public IPickableObject[] items;
    public Image[] inventoryItems;

    private int inventorySlotCount = 3;

    public event EventHandler<int> OnItemAdd;

    public void Awake()
    {
        //currentInventoryItemCount = 0;
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
                OnItemAdd?.Invoke(this, slotNumber);
                Debug.Log("hi");
                return true;
            }
        }

        return false;

    }

    public void RemoveItemFromInventory(object sender, int slotNumber)
    {
        items[slotNumber].ChangePosition(this.gameObject.transform.position.x, this.transform.position.y - 1.07f); //1.07 is distance from playerObject to floor
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
