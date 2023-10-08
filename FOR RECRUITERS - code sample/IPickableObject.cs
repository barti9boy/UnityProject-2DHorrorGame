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
    public AnimationClip PickupAnimationClip
    {
        get;
    }
    public void ChangePosition(float x, float y); //function used to drop item from inventory in correct place
    public void DestroyItem();
}
