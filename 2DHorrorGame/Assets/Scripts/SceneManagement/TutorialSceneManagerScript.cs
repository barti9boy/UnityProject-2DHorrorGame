using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class TutorialSceneManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject gameManger;

    public GameObject pauseMenu;
    public GameObject popup;
    public GameObject introduction;

    private GameObject player;
    public GameObject playerUI;
    public PlayerUIScript playerUIScript;

    public GameObject FlashlightButton;
    public GameObject InteractionButton;

    public GameObject monster;
    public List<GameObject> tutorialPopups;
    public GameObject key;
    public DoorScript exitDoor;
    public List<GameObject> lights;

    private bool displayedIntro = false;
    private bool doorPopupActive = false;
    private bool monsterPopupActive = false;
    private bool lightPopupActive = false;
    private bool unlockPopupActive = false;



    private void Awake()
    {
        gameManger.GetComponent<GameManagerScript>().OnAfterPlayerLoaded += TutorialSceneManagerScript_OnAfterPlayerLoaded;
        playerUIScript = playerUI.GetComponent<PlayerUIScript>();
    }

    private void TutorialSceneManagerScript_OnAfterPlayerLoaded(object sender, GameObject e)
    {
        player = e;
    }

    private void Start()
    {
        tutorialPopups[5].SetActive(false);
        tutorialPopups[3].SetActive(false);
        tutorialPopups[4].SetActive(false);
        InteractionButton.SetActive(false);
        FlashlightButton.SetActive(false);
    }
    private void Update()
    {
        if (pauseMenu.activeInHierarchy || popup.activeInHierarchy || introduction.activeInHierarchy)
        {
            StopGame();
        }
        else ResumeGame();

        if(!introduction.activeInHierarchy && !displayedIntro)
        {
            playerUIScript.notificationText.text = "ESCAPE THE BASEMENT";
            playerUIScript.currentTime = -0.6f;
            displayedIntro = true;
        }
        if(key.activeSelf && !tutorialPopups[1].activeSelf)
        {
            if(!doorPopupActive)
            {
                playerUIScript.notificationText.text = "FIND THE BASEMENT KEY";
                playerUIScript.currentTime = -0.6f;
                doorPopupActive = true;
            }
            tutorialPopups[1].SetActive(false);
        }
        if(!tutorialPopups[2].activeSelf && key.activeSelf)
        {
            InteractionButton.SetActive(true);
        }
        if(!key.activeSelf && !monsterPopupActive)
        {
            if(tutorialPopups[1].activeSelf)
            {
                tutorialPopups[1].SetActive(false);
            }
            tutorialPopups[3].SetActive(true);
            foreach (GameObject light in lights)
            {
                light.SetActive(false);
            }
            tutorialPopups[4].SetActive(true);
            monsterPopupActive = true;
        }
        if(tutorialPopups[4].activeSelf && !lightPopupActive)
        {
            FlashlightButton.SetActive(true);
            lightPopupActive = true;
        }
        if(!tutorialPopups[3].activeSelf && monsterPopupActive && !unlockPopupActive)
        {
            monster.SetActive(true);
            tutorialPopups[5].SetActive(true);
            unlockPopupActive = true;
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
