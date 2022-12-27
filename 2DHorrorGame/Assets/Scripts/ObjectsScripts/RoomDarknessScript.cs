using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public class RoomDarknessScript : MonoBehaviour
{
    [SerializeField] private List<PressurePlateScript> pressurePlates = new List<PressurePlateScript>();
    private PhotonView photonView;
    private void Awake()
    {
        foreach (PressurePlateScript plate in pressurePlates)
        {
            plate.OnPlayerStand += Plate_OnPlayerStand;
            plate.OnPlayerLeave += Plate_OnPlayerLeave;
        }
        photonView = GetComponent<PhotonView>();
    }

    private void Plate_OnPlayerLeave(object sender, EventArgs e)
    {
        this.gameObject.SetActive(true);
        photonView.RPC("RPC_OnPlayerLeave", RpcTarget.All, photonView.ViewID);
    }

    private void Plate_OnPlayerStand(object sender, EventArgs e)
    {
        this.gameObject.SetActive(false);
        photonView.RPC("RPC_OnPlayerStand", RpcTarget.All, photonView.ViewID);
    }

    [PunRPC]
    private void RPC_OnPlayerLeave(int viewID)
    {
        this.gameObject.SetActive(true);
        if (photonView.ViewID == viewID)
        {
            this.gameObject.SetActive(true);
        }
    }
    [PunRPC]
    private void RPC_OnPlayerStand(int viewID)
    {
        this.gameObject.SetActive(false);
        if (photonView.ViewID == viewID)
        {
            this.gameObject.SetActive(false);
        }
    }
}
