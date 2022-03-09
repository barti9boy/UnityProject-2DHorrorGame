using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class InventoryItemScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isEmpty;
    public int slotNumber;
    [SerializeField] private bool isBeingClicked;
    [SerializeField] private float requiredHoldTime;
    [SerializeField] private float currentHoldTime;

    public event EventHandler<int> OnItemDrop;

    public void Awake()
    {
        isBeingClicked = false;
        currentHoldTime = 0;
        requiredHoldTime = 1.2f;
    }
    private void Update()
    {
        if (isBeingClicked)
        {
            currentHoldTime += Time.deltaTime;
            if (currentHoldTime >= requiredHoldTime)
            {
                OnItemDrop?.Invoke(this, slotNumber);
                isBeingClicked = false;
            }
        }
        else currentHoldTime = 0;
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        isBeingClicked = true;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        isBeingClicked = false;
    }
}
