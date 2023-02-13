using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PopupTriggerScript : MonoBehaviour
{
    public GameObject popup;
    private PopupScript popupScript;
    [Header("Input message in CAPS")]
    private string message;
    private void Awake()
    {
        popupScript = popup.GetComponent<PopupScript>();
        message = "LAB NOTES; DAY 1543 \n \n ALL OF THE SUBJECTS ESCAPED THEIR LABORATORY CELLS AND GOT FREE \n MANY TURNED INTO UNDIFINED CREATURES AND ARE HOSTILE TOWARDS ANY LIVING BEEINGS. \n THEY ARE SENSITIVE TO LIGHT, SO FLASHLIGHT [F] HAS TO BE USE CAUTIOUSLY" +
            "\n THE POWER IN FLASHLIGTS RUN OUT QUICKLY SO USE IT IS REQUIRED TO USE THEM PRUDENTLY";


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.GetComponent<PhotonView>().IsMine)
            {
                popupScript.PopupSetActive(message);
                AudioManager.Instance.PlaySoundAtPosition(Clip.NotePickUp, transform.position);
                this.gameObject.SetActive(false);
            }
            
        }
        
    }


}
