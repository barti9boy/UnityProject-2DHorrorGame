using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightIndycatorScript : MonoBehaviour
{
    
    [SerializeField] private GameObject gameManger;
    [SerializeField] private Image radialIndicatorUI;

    private float currentBettery;
    private GameObject playerObject;
    private GameObject flashlight;
    private GameObject indycator;
    
    void Awake()
    {
        gameManger.GetComponent<GameManagerScript>().OnAfterPlayerLoaded += FlashlightIndycatorScript_OnAfterPlayerLoaded;
        indycator = gameObject.transform.GetChild(0).gameObject;
        indycator.SetActive(false);

    }

    private void FlashlightIndycatorScript_OnAfterPlayerLoaded(object sender, GameObject e)
    {
        playerObject = e;
        flashlight = playerObject.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(flashlight)
        {

        
        if (flashlight.activeSelf == true)
        {
            indycator.SetActive(true);
            currentBettery = 1 - (playerObject.GetComponent<PlayerStateMachine>().batteryTimer / playerObject.GetComponent<PlayerStateMachine>().timeOfBattery);
            radialIndicatorUI.fillAmount = currentBettery;
        }
        else 
        {
            indycator.SetActive(false);
        }
        }
    }
}
