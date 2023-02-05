using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PauseMenuScript : MonoBehaviourPunCallbacks
{
    public static bool isGamePaused = false;
    public GameObject popup;
    private PopupScript popupScript;
    [SerializeField] private Button confirmButton;
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

    public void GoToMenuButton()
    {
        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(GoToMenu);
        SetPopup();
        
    }
    public void QuitGameButton()
    {
        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(QuitGame);
        SetPopup();
        
    }
    public void SetPopup()
    {
        message = "ARE YOU SURE YOU WANT TO QUIT? \n ALL PLAYERS WILL BE DISCONNECTED";
        popupScript.PopupSetActive(message);
    }

    public void GoToMenu()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        PhotonNetwork.Disconnect();
        Application.Quit();
    }
}


