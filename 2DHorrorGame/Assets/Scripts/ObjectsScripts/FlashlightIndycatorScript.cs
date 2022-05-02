using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightIndycatorScript : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private Image radialIndicatorUI;

    private float currentBettery;
    private GameObject flashlight;
    private GameObject indycator;
    
    void Awake()
    {
        indycator = gameObject.transform.GetChild(0).gameObject;
        flashlight = playerObject.transform.GetChild(1).gameObject;
        indycator.SetActive(false);

    }

    // Update is called once per frame
    void Update()
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
