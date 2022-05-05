using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSceneManager : MonoBehaviour
{
    public GameObject pauseMenu;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenu.activeInHierarchy)
        {
            StopGame();
        }
        else ResumeGame();
    }
    public void StopGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
