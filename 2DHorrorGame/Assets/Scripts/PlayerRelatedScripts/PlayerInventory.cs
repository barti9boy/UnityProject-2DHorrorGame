using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Photon.Pun;

public class ItemEventArgs
{
    public int inventorySlotNumber;
    public IPickableObject inventoryItem;
}


public class PlayerInventory : MonoBehaviour
{
    public static Action<int> OnBatteryChanged;
    public IPickableObject[] items;
    public Image[] inventoryItems;

    private PhotonView photonView;
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
        if (photonView.IsMine)
        {
            for (int slotNumber = 0; slotNumber < inventorySlotCount; slotNumber++)
            {
                inventoryItems[slotNumber].enabled = false;
                inventoryItems[slotNumber].GetComponent<InventoryItemScript>().slotNumber = slotNumber;
            }
        }
        photonView = GetComponent<PhotonView>();
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
        
        for(int i = 2; i >= 0; i--)
        {
            if(items[i] != null)
            {
                if (items[i].DisplayName == "Battery")
                {
                    DestroyItemFromInventory(i);
                    OnBatteryChanged(i);
                    PlayerBatteries--;
                    break;
                }
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

            int itemViewId = items[slotNumber].PhotonView.ViewID;
            items[slotNumber].ChangePosition(this.gameObject.transform.position.x, this.transform.position.y - 1.07f); //1.07 is the distance from playerObject to floor
            items[slotNumber] = null;
            photonView.RPC("RPC_RemoveItemFromInventory", RpcTarget.Others, photonView.ViewID, itemViewId);

        }
    }

    public void DestroyItemFromInventory(int slotNumber)
    {
        if(items[slotNumber] != null)
        {
            Debug.Log($"Item {items[slotNumber]} destroyed");
            int itemViewId = items[slotNumber].PhotonView.ViewID;
            items[slotNumber].DestroyItem();
            items[slotNumber] = null;
            photonView.RPC("RPC_DestroyItemFromInventory", RpcTarget.Others, photonView.ViewID, itemViewId);
        }
    }


    [PunRPC]
    private void RPC_RemoveItemFromInventory(int viewId, int itemViewId)
    {
        if(photonView.ViewID == viewId)
        {
            var item = PhotonView.Find(itemViewId);
            item.GetComponent<IPickableObject>().ChangePosition(this.gameObject.transform.position.x, this.transform.position.y - 1.07f);
        }
    }

    [PunRPC]
    private void RPC_DestroyItemFromInventory(int viewId, int itemViewId)
    {
        if (photonView.ViewID == viewId)
        {
            var item = PhotonView.Find(itemViewId);
            item.GetComponent<IPickableObject>().DestroyItem();
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
