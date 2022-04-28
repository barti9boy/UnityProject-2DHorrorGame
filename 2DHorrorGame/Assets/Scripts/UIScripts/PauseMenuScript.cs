using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool isGamePaused = false;

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

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
