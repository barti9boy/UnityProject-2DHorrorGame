using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class PauseMenuScript : MonoBehaviourPunCallbacks
{
    public static bool isGamePaused = false;
    public GameObject popup;
    private PopupScript popupScript;
    [Header("Input message in CAPS")]
    [SerializeField] private string message;

    private void Awake()
    {
        popupScript = popup.GetComponent<PopupScript>();
    }

    public void Pause()
    {
        this.gameObject.SetActive(true);
    }
    public void Resume()
    {
        this.gameObject.SetActive(false);
    }

    public void GoToMenu()
    {
        DisconnectPlayers();
    }
    public void QuitGame()
    {
        message = "ARE YOU SURE YOU WANT TO QUIT? \n ALL PLAYERS WILL BE DISCONNECTED";
        popupScript.PopupSetActive(message);
        
    }
    public void DisconnectPlayers()
    {
        Application.Quit();
    }
}
