using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoroutineHandler : MonoBehaviour
{
    public static CoroutineHandler Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public IEnumerator WaitUntilAnimated(float timeOfAnimation, Action OnFinish)
    {
        Debug.Log("Coroutine started");
        yield return new WaitForSeconds(timeOfAnimation);
        Debug.Log("Coroutine finished");
        OnFinish();
    }
}
