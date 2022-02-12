using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    private CapsuleCollider2D playerCollider;
    private bool isInVent;
    private void Start()
    {
        playerCollider = gameObject.GetComponent<CapsuleCollider2D>();
        isInVent = false;
        GameObject.FindGameObjectWithTag("Ladder").GetComponent<LadderScript>().OnVentEnterOrLeave += OnVentEnterOrLeave;
    }

    private void OnVentEnterOrLeave(object sender, EventArgs e)
    {
        isInVent = !isInVent;
        if(isInVent)
        {
            playerCollider.size = new Vector2(1.26f, 2.1f);
        }
        if(!isInVent)
        {
            playerCollider.size = new Vector2(1f, 2.1f);
        }
    }

}
