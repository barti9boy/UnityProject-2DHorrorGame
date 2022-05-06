using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InteractionButtonHighlightDoors : MonoBehaviour
{
    public GameObject interactionButton;
    public Sprite unlockButtonSprite;
    public Sprite openButtonSprite;
    public Sprite defaultButtonSprite;

    private Image interactionButtonImage;
    public bool isLocked;
    private bool changed;
    private void Awake()
    {
        isLocked = GetComponentInParent<DoorScript>().isLocked;
        interactionButtonImage = interactionButton.GetComponent<Image>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isLocked = GetComponentInParent<DoorScript>().isLocked;
        if (collision.gameObject.CompareTag("Player"))
        {
            try
            {
                if (isLocked) interactionButtonImage.sprite = unlockButtonSprite;
                else interactionButtonImage.sprite = openButtonSprite;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isLocked = GetComponentInParent<DoorScript>().isLocked;
        }
        if(!isLocked)
        {
            interactionButtonImage.sprite = openButtonSprite;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            try
            {
                interactionButtonImage.sprite = defaultButtonSprite;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
