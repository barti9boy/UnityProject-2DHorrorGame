using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickableObject
{
    public int ItemID
    {
        get;
    }
    public string DisplayName
    {
        get;
    }
    public Sprite InventoryIcon
    {
        get;
    }
    public void ChangePosition(float x, float y);
}
