using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSceneManagerScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject popup;
    public GameObject introduction;

    public GameObject player;
    public PlayerStateMachine playerStateMachine;
    public GameObject playerUI;
    public PlayerUIScript playerUIScript;

    public GameObject FlashlightButton;
    public GameObject InteractionButton;

    public GameObject monster;
    public List<GameObject> tutorialPopups;
    public GameObject key;
    public DoorScript exitDoor;
    public List<GameObject> lights;

    private bool diplayedIntro = false;
    private bool doorPopupActive = false;
    private bool monsterPopupActive = false;
    private bool lightPopupActive = false;
    private void Awake()
    {
        player.GetComponent<PlayerStateMachine>();
        playerUIScript = playerUI.GetComponent<PlayerUIScript>();
    }
    private void Start()
    {
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

        if(!introduction.activeInHierarchy && !diplayedIntro)
        {
            playerUIScript.notificationText.text = "ESCAPE THE BASEMENT";
            playerUIScript.currentTime = -0.6f;
            diplayedIntro = true;
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
        if(!tutorialPopups[3].activeSelf && monsterPopupActive)
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
