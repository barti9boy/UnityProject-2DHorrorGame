using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class InventoryItemScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isEmpty;
    [SerializeField] private bool isBeingClicked;
    [SerializeField] private float requiredHoldTime;
    [SerializeField] private float currentHoldTime;
    [SerializeField] private Image image;

    public event EventHandler OnItemDrop;

    public void Awake()
    {
        isEmpty = true;
        isBeingClicked = false;
        currentHoldTime = 0;
        requiredHoldTime = 1f;
        image = this.gameObject.GetComponent<Image>();
    }
    private void Update()
    {
        if (isBeingClicked)
        {
            currentHoldTime += Time.deltaTime;
            if (currentHoldTime >= requiredHoldTime)
            {
                isEmpty = true;
                OnItemDrop?.Invoke(this, EventArgs.Empty);
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
