using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE: when adding this prefab you have to set sorting layer and sprite  manually in inspector
public class InteractionHighlight : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Material material;
    private void Awake()
    {
        spriteRenderer.sprite = GetComponentInParent<SpriteRenderer>().sprite;
        spriteRenderer.sortingOrder = GetComponentInParent<SpriteRenderer>().sortingOrder + 1;
        spriteRenderer.sortingLayerName = GetComponentInParent<SpriteRenderer>().sortingLayerName;
        spriteRenderer.color = new Color(255,255,255, 0.35f);
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
