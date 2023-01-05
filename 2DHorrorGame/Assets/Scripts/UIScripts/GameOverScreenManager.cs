using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverScreenManager : MonoBehaviour
{
    public string sceneName;
    [SerializeField] private GameObject gameOverScreen;

    private void Awake()
    {
        PlayerStateDead.OnGameOver += GameOver;
    }

    private void OnDestroy()
    {
        PlayerStateDead.OnGameOver -= GameOver;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        CoroutineHandler.Instance.StartCoroutine(CoroutineHandler.Instance.WaitUntilAnimated(2f,
            () => 
            RestartButton()));
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ExitButton()
    {

    }
}
