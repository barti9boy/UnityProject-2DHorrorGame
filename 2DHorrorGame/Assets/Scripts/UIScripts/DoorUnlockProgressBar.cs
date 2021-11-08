using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DoorUnlockProgressBar : MonoBehaviour
{

    [SerializeField] private float currentProgress;
    [SerializeField] private GameObject door;
    [SerializeField] private float timeToUnlock;
    [SerializeField] private Image radialIndicatorUI;


    private void Awake()
    {
        door = transform.parent.parent.gameObject;
        currentProgress = 0;
        timeToUnlock = door.GetComponent<DoorScript>().unlockTimeRequired;
    }
    private void Update()
    {
        if(door.GetComponent<DoorScript>().isLocked)
        {
            currentProgress = door.GetComponent<DoorScript>().interactionTime / timeToUnlock;
            radialIndicatorUI.fillAmount = currentProgress;
        }
        else
        {

            this.gameObject.SetActive(false);
        }
    }
}
