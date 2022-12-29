using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverScript : MonoBehaviour
{
    public string sceneName;

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
        gameObject.SetActive(true);
        CoroutineHandler.Instance.WaitUntilAnimated(2f, () => RestartButton());
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ExitButton()
    {

    }
}
