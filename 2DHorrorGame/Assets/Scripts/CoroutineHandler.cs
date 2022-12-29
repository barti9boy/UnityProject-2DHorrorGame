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

    public void Lerp(float startValue, float endValue, float speed, Action<float> OnNewValue, Action<float> OnEnd)
    {
        StartCoroutine(LerpCoroutine(startValue, endValue, speed, OnNewValue, OnEnd));
    }

    public IEnumerator WaitUntilAnimated(float timeOfAnimation, Action OnFinish)
    {
        Debug.Log("Coroutine started");
        yield return new WaitForSeconds(timeOfAnimation);
        Debug.Log("Coroutine finished");
        OnFinish();
    }

    IEnumerator LerpCoroutine(float startValue, float endValue, float speed, Action<float> OnNewValue, Action<float> OnEnd)
    {
        float timeElapsed = 0;
        float newValue;
        float lerpDuration = Mathf.Abs(endValue - startValue) / speed;  // t = s/v
        while (timeElapsed < lerpDuration)
        {
            newValue = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            OnNewValue?.Invoke(newValue);
            yield return null;
        }
        newValue = endValue;
        OnNewValue?.Invoke(newValue);
        OnEnd?.Invoke(newValue);
    }
}
