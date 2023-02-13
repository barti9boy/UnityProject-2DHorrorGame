using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTriggerScript : MonoBehaviour
{
    
    public GameObject popup;
    private PopupScript popupScript;
    [Header("Input message in CAPS")]
    private string message;
    private void Awake()
    {
        popupScript = popup.GetComponent<PopupScript>();
        message = "DAY 1543, ID:203 \n ";


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            popupScript.PopupSetActive(message);
            AudioManager.Instance.PlaySoundAtPosition(Clip.NotePickUp, transform.position);
        }
        this.gameObject.SetActive(false);
    }


}
