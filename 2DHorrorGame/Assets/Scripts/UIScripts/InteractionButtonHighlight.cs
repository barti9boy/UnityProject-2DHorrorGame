using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InteractionButtonHighlight : MonoBehaviour
{
    public GameObject interactionButton;
    public Sprite interactionButtonSprite;
    public Sprite defaultButtonSprite;

    private Image interactionButtonImage;
    private void Awake()
    {
        interactionButtonImage = interactionButton.GetComponent<Image>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            try
            {
                interactionButtonImage.sprite = interactionButtonSprite;
            }
            catch (Exception e)
            {
                throw e;
            }

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
