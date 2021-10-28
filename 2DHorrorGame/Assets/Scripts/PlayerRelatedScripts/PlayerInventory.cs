using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<int> inventoryItemsIDs;
    public void Awake()
    {
        inventoryItemsIDs = new List<int>();
    }

    public void AddItemToInventory(int itemID)
    {
        inventoryItemsIDs.Add(itemID);
    }
    public void DebugLogInventory()
    {
        foreach(int i in inventoryItemsIDs)
        {
            Debug.Log(i.ToString() + ", ");
        }

    }
}
