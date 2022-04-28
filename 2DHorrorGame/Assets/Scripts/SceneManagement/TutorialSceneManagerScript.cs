using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSceneManagerScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject popup;
    public GameObject introduction;

    public GameObject player;
    public PlayerStateMachine playerStateMachine;
    public GameObject playerUI;
    public PlayerUIScript playerUIScript;
    public GameObject monster;
    public List<GameObject> tutorialPopups;
    public GameObject key;
    public DoorScript exitDoor;

    private bool displayed = false;
    private bool monsterAlertShown = false;
    private void Awake()
    {
        player.GetComponent<PlayerStateMachine>();
        playerUIScript = playerUI.GetComponent<PlayerUIScript>();
    }
    private void Start()
    {
        tutorialPopups[3].SetActive(false);
    }
    private void Update()
    {
        if (pauseMenu.activeInHierarchy || popup.activeInHierarchy || introduction.activeInHierarchy)
        {
            StopGame();
        }
        else ResumeGame();

        if(!key.activeSelf && tutorialPopups[1].activeSelf)
        {
            tutorialPopups[1].SetActive(false);
        }
        if(key.activeSelf && !tutorialPopups[1].activeSelf)
        {
            if(!displayed)
            {
                playerUIScript.notificationText.text = "FIND THE BASEMENT KEY";
                playerUIScript.currentTime = 0;
                displayed = true;
            }
            tutorialPopups[1].SetActive(false);
        }
        if(!key.activeSelf && !monsterAlertShown)
        {
            tutorialPopups[3].SetActive(true);
            monsterAlertShown = true;
        }
        if(!tutorialPopups[3].activeSelf && monsterAlertShown)
        {
            monster.SetActive(true);
        }

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
