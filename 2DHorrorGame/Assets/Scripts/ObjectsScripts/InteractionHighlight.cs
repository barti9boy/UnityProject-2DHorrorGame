using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE: when adding this prefab you have to set sorting layer and sprite  manually in inspector
public class InteractionHighlight : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Material material;

    private float colorAlpha = 0.15f;
    private void Awake()
    {
        spriteRenderer.sprite = GetComponentInParent<SpriteRenderer>().sprite;
        spriteRenderer.sortingOrder = GetComponentInParent<SpriteRenderer>().sortingOrder + 1;
        spriteRenderer.sortingLayerName = GetComponentInParent<SpriteRenderer>().sortingLayerName;
        spriteRenderer.color = new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, colorAlpha);
        spriteRenderer.material = material;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spriteRenderer.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spriteRenderer.enabled = false;
        }
    }
}
