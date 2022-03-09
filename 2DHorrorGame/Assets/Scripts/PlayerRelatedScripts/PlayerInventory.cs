using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;


public class PlayerInventory : MonoBehaviour
{
    public IPickableObject[] items;
    public Image[] inventorySlots;
    public Image[] inventoryItems;
    public Sprite itemContainer;

    public TextMeshProUGUI notificationText;

    private int inventorySlotCount = 3;
    public int currentInventoryItemCount;
    private InventoryItemScript inventoryItemScript;

    public void Awake()
    {
        notificationText.text = "";
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
            inventoryItemScript.OnItemDrop += RemoveItemFromInventory;
        }
    }

    public bool AddItemToInventory(IPickableObject item)
    {
        if (currentInventoryItemCount < inventorySlotCount)
        {
            for (int slotNumber = 0; slotNumber < inventorySlotCount; slotNumber++)
            {
                if (inventoryItems[slotNumber].sprite == null)
                {
                    inventoryItems[slotNumber].sprite = item.InventoryIcon;
                    inventoryItems[slotNumber].enabled = true;
                    inventoryItems[slotNumber].GetComponent<InventoryItemScript>().isEmpty = false;
                    items[slotNumber] = item;
                    Debug.Log(items[slotNumber].ItemID);
                    currentInventoryItemCount++;
                    return true;
                }
            }
            return false;
        }
        else
        {
            //send info to UI probably idk
            notificationText.text = "Could not pick up " + item.DisplayName + ", inventory full!";
            StartCoroutine(MessageNotification());
            return false;
        }
    }

    public void RemoveItemFromInventory(object sender, EventArgs e)
    {
        for(int slotNumber = 0; slotNumber <inventorySlotCount; slotNumber++)
        {
            if (items[slotNumber] != null && inventoryItems[slotNumber].GetComponent<InventoryItemScript>().isEmpty == true)
            {
                inventoryItems[slotNumber].sprite = null;
                inventoryItems[slotNumber].enabled = false;
                if (currentInventoryItemCount > 0) currentInventoryItemCount--;
                items[slotNumber].ChangePosition(this.gameObject.transform.position.x, this.transform.position.y - 1.07f);
                items[slotNumber] = null;
            }
        }

    }

    IEnumerator MessageNotification()
    {
        yield return new WaitForSeconds(1f);
        notificationText.text = "";
    }





    public void DebugLogInventory()
    {
        foreach(IPickableObject i in items)
        {
            Debug.Log(i.ItemID.ToString() + ", ");
        }
    }
}
