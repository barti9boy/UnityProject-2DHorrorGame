using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour, IPickableObject
{
    [SerializeField] private int itemID;
    [SerializeField] private string displayName;

    public int ItemID { get; private set; }

    public string DisplayName { get; private set; }

    public void Awake()
    {
        ItemID = itemID;
        DisplayName = displayName;
    }
}
