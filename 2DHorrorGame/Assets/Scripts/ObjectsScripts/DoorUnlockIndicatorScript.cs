using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorUnlockIndicatorScript : MonoBehaviour
{

    [SerializeField] private float currentProgress;
    private DoorScript doorScript;
    [SerializeField] private float timeToUnlock;
    [SerializeField] private Image indicatorUI;
    [SerializeField] private Image indicatorUIBackground;
    [SerializeField] private bool indicatorDirection;


    private void Awake()
    {
        doorScript = transform.parent.parent.gameObject.GetComponent<DoorScript>();
        currentProgress = 0;
        timeToUnlock = doorScript.unlockTimeRequired;

    }
    private void Update()
    {
        if (doorScript.isLocked)
        {
            currentProgress = doorScript.interactionTime / timeToUnlock;
            if(currentProgress > 0)
            {
                indicatorUIBackground.enabled = true;
            }
            else
            {
                indicatorUIBackground.enabled = false;
            }
            indicatorUI.fillAmount = currentProgress;
        }
        else
        {
            indicatorUIBackground.enabled = false;
            this.gameObject.SetActive(false);
        }
    }
}
