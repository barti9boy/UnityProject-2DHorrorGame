using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
    public PhotonView PhotonView
    {
        get;
    }
    public void ChangePosition(float x, float y);
    public void DestroyItem();
}
