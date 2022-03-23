using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool isGamePaused = false;

    public void Pause()
    {
        isGamePaused = true;
        this.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        isGamePaused = false;
        this.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToMenu()
    {

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
