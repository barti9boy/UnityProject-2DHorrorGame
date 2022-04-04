using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;



public class FlickingLight : MonoBehaviour
{
    public bool flickIntensity;
    public float minFlickTime;
    public float maxFlickTime;
    public float intensityRange;

    public bool flickColor;
    public float colorTimeMin;
    public float colorTimeMax;
    public float colorRadius;

    public Light2D objectLight;
    private float baseIntensity;


    private Color _baseColor;
    private Color _color;
    private Vector3 _colorVector;
    void Start()
    {
        //objectLight = gameObject.GetComponent<Light2D>();
        baseIntensity = objectLight.intensity;
        _baseColor = objectLight.color;
        flickIntensity = true;
        StartCoroutine(FlickIntensity());
        StartCoroutine(FlickColor());
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
    private IEnumerator FlickColor()
    {
        float t0 = Time.time;
        float t = t0;
        WaitUntil wait = new WaitUntil(() => Time.time > t0 + t);
        yield return new WaitForSeconds(Random.Range(0.01f, 0.5f));

        while (true)
        {
            if (flickColor)
            {
                t0 = Time.time;
                ColorToVector3(objectLight.color);
                Vector3ToColor(Random.insideUnitSphere * colorRadius + _colorVector);
                objectLight.color = _color;
                t = Random.Range(colorTimeMin, colorTimeMax);
                yield return wait;
                objectLight.color = _baseColor;
            }
            else yield return null;
        }
    }

    void Vector3ToColor(Vector3 v)
    {
        _color.r = v.x;
        _color.g = v.y;
        _color.b = v.z;
    }

    void ColorToVector3(Color c)
    {
        _colorVector.x = c.r;
        _colorVector.y = c.g;
        _colorVector.z = c.b;
    }
}
