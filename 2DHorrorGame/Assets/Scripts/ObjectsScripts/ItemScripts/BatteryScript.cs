using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : MonoBehaviour, IPickableObject
{
    [SerializeField] private int itemID;
    [SerializeField] private string displayName;
    [SerializeField] private Sprite inventoryIcon;
    [SerializeField] private AnimationClip pickupAnimationClip;

    public int ItemID { get; private set; }

    public string DisplayName { get; private set; }

    public Sprite InventoryIcon { get; private set; }

    public AnimationClip PickupAnimationClip { get; private set; }

    private void Awake()
    {
        ItemID = itemID;
        DisplayName = displayName;
        InventoryIcon = inventoryIcon;
        PickupAnimationClip = pickupAnimationClip;
    }
    public void ChangePosition(float x, float y)
    {
        this.gameObject.SetActive(true);
        this.gameObject.transform.position = new Vector2(x, y);
    }
    public void DestroyItem()
    {
        Destroy(this.gameObject);
    }
}
