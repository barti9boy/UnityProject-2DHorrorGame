using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InteractionButtonHighlightVents : MonoBehaviour
{
    public GameObject player;
    public GameObject interactionButton;
    public Sprite goUpButtonSprite;
    public Sprite goDownButtonSprite;
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
                if (player.transform.position.y < transform.position.y) interactionButtonImage.sprite = goUpButtonSprite;
                else interactionButtonImage.sprite = goDownButtonSprite;
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
            if (player.transform.position.y < transform.position.y)
            {
                interactionButtonImage.sprite = goUpButtonSprite;
            }
            else interactionButtonImage.sprite = goDownButtonSprite;
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
