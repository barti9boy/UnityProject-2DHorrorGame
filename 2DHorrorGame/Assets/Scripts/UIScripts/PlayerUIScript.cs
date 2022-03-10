using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class PlayerUIScript : MonoBehaviour
{
    public GameObject player;

    public IPickableObject[] items;
    public Image[] inventorySlots;
    public Image[] inventoryItems;
    public Sprite itemContainer;

    public TextMeshProUGUI notificationText;

    private int inventorySlotCount = 3;

    void Awake()
    {
        
        player.GetComponent<PlayerInventory>().OnItemAdd += AddItemToInventory;
        items = player.GetComponent<PlayerInventory>().items;
        notificationText.text = "text box";
        for (int i = 0; i < inventorySlotCount; i++)
        {
            inventorySlots[i].sprite = itemContainer;
        }
        foreach (Image image in inventoryItems)
        {
            image.enabled = false;
            image.GetComponent<InventoryItemScript>().OnItemDrop += RemoveItemFromInventory;
        }
    }

    void Update()
    {
        
    }

    public void AddItemToInventory(object sender, int slotNumber)
    {
        items = player.GetComponent<PlayerInventory>().items;
        if (inventoryItems[slotNumber].sprite == null)
        {
            inventoryItems[slotNumber].sprite = items[slotNumber].InventoryIcon;
            inventoryItems[slotNumber].enabled = true;
        }
    }
    public void RemoveItemFromInventory(object sender, int slotNumber)
    {
            inventoryItems[slotNumber].sprite = null;
            inventoryItems[slotNumber].enabled = false;
    }

    IEnumerator MessageNotification()
    {
        yield return new WaitForSeconds(1f);
        notificationText.enabled = false;
    }
}
