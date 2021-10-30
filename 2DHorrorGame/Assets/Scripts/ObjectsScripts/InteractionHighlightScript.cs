using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class InteractionHighlightScript : MonoBehaviour
{
    [SerializeField] private float highlightIntensity;
    private Light2D interactionHighlight;
    private Collider2D highlightTriggerArea;

    private void Awake()
    {
        interactionHighlight = GetComponent<Light2D>();
        highlightTriggerArea = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            interactionHighlight.intensity = highlightIntensity;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactionHighlight.intensity = 0;
        }
    }
}
