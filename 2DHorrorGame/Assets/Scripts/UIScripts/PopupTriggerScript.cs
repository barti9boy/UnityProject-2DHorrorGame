using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTriggerScript : MonoBehaviour
{
    
    public GameObject popup;
    private PopupScript popupScript;
    [Header("Input message in CAPS")]
    [SerializeField] private string message;
    private void Awake()
    {
        popupScript = popup.GetComponent<PopupScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            popup.SetActive(true);
            popupScript.PopupSetMessage(message);
            popupScript.PopupIn();
            this.gameObject.SetActive(false);
        }
    }
}
