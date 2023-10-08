using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class PlayerUIScript : MonoBehaviour
{
    public GameObject player;
    //----------Inventory elements----------//
    public Image[] inventorySlots;
    public Image[] inventoryItems;
    public Sprite itemContainer;
    private int inventorySlotCount = 3;
    //----------Notification textbox elements----------//
    public TextMeshProUGUI notificationText;
    private static float notificationTime = 1.15f;
    public float currentTime;



    void Awake()
    {
        currentTime = 0;
        player.GetComponent<PlayerInventory>().OnItemAdd += AddItemToInventory;
        player.GetComponent<PlayerInventory>().OnItemSendNotification += ShowNotification;
        notificationText.text = "";
        for (int i = 0; i < inventorySlotCount; i++)
        {
            inventorySlots[i].sprite = itemContainer;
        }
        foreach (Image image in inventoryItems)
        {
            image.enabled = false;
            image.GetComponent<InventoryItemSlotScript>().OnItemDrop += RemoveItemFromInventory;
        }
    }
    private void Update()
    {
        if(notificationText.text != "")
        {
            currentTime += Time.deltaTime;
            if (currentTime >= notificationTime) notificationText.text = "";
        }
    }

    private void ShowNotification(object sender, string e)
    {
        currentTime = 0;
        notificationText.text = e;
    }

    public void AddItemToInventory(object sender, ItemEventArgs args)
    {
        if (inventoryItems[args.inventorySlotNumber].sprite == null)
        {
            inventoryItems[args.inventorySlotNumber].sprite = args.inventoryItem.InventoryIcon;
            inventoryItems[args.inventorySlotNumber].enabled = true;
        }
    }
    public void RemoveItemFromInventory(object sender, int slotNumber)
    {
            inventoryItems[slotNumber].sprite = null;
            inventoryItems[slotNumber].enabled = false;
    }
}
