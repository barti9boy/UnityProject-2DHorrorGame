using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;



public class FlickingLight : MonoBehaviour
{
    public float minFlickTime;
    public float maxFlickTime;
    public float intensityRange;
    public bool flickIntensity;

    public Light2D objectLight;
    private float baseIntensity;

    void Start()
    {
        //objectLight = gameObject.GetComponent<Light2D>();
        baseIntensity = objectLight.intensity;
        flickIntensity = true;
        StartCoroutine(FlickIntensity());
    }

    void Update()
    {
        
    }
    private IEnumerator FlickIntensity()
    {
        float t0 = Time.time;
        float t = t0;
        WaitUntil wait = new WaitUntil(() => Time.time > t0 + t);
        yield return new WaitForSeconds(Random.Range(0.01f, 0.5f));

        while (true)
        {
            if (flickIntensity)
            {
                t0 = Time.time;
                float r = Random.Range(baseIntensity - intensityRange, baseIntensity + intensityRange);
                objectLight.intensity = r;
                t = Random.Range(minFlickTime, maxFlickTime);
                yield return wait;
            }
            else yield return null;
        }
    }
}
