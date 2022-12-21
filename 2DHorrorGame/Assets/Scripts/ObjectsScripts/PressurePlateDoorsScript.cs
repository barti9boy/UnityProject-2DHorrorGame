using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public class PressurePlateDoorScript : MonoBehaviour
{
    [SerializeField] private List<PressurePlateScript> pressurePlates = new List<PressurePlateScript>();
    private Animator animator;
    private Collider2D collider;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        foreach(PressurePlateScript plate in pressurePlates)
        {
            plate.OnPlayerStand += Plate_OnPlayerStand;
            plate.OnPlayerLeave += Plate_OnPlayerLeave;
        }
    }

    private void Plate_OnPlayerLeave(object sender, EventArgs e)
    {
        collider.gameObject.SetActive(false);
    }

    private void Plate_OnPlayerStand(object sender, EventArgs e)
    {
        collider.gameObject.SetActive(true);
    }
}
