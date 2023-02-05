using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MainMenuScript : MonoBehaviour
{
    public void PlayButton()
    {
        if (!PhotonNetwork.IsConnectedAndReady) 
        {
            SceneManager.LoadScene("LoadingScene");
        }
        else
        {
            SceneManager.LoadScene("LobbyScene");
        }
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
