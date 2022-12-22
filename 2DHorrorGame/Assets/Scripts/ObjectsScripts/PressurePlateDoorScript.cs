using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public class PressurePlateDoorScript : MonoBehaviour
{
    [SerializeField] private List<PressurePlateScript> pressurePlates = new List<PressurePlateScript>();
    private Collider2D collider;

    private Animator animator;
    private int animOpenDoor;
    private int animCloseDoor;

    private PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        foreach(PressurePlateScript plate in pressurePlates)
        {
            plate.OnPlayerStand += Plate_OnPlayerStand;
            plate.OnPlayerLeave += Plate_OnPlayerLeave;
        }
    }
    private void Start()
    {
        AssignAnimationIDs();
    }
    private void AssignAnimationIDs()
    {
        animOpenDoor = Animator.StringToHash("TriggerOpenDoor");
        animCloseDoor = Animator.StringToHash("TriggerCloseDoor");
    }
    private void Plate_OnPlayerLeave(object sender, EventArgs e)
    {
        collider.enabled = true;
        PlayCloseDoorAnim();
    }

    private void Plate_OnPlayerStand(object sender, EventArgs e)
    {
        collider.enabled = false;
        PlayOpenDoorAnim();
    }


    public void PlayOpenDoorAnim()
    {
        animator.SetTrigger(animOpenDoor);
        animator.ResetTrigger(animCloseDoor);
        photonView.RPC("RPC_PlayOpenDoorAnim", RpcTarget.Others, photonView.ViewID);
        Debug.Log("PlayOpenDoorAnim RPC sent");
    }
    public void PlayCloseDoorAnim()
    {
        animator.SetTrigger(animCloseDoor);
        animator.ResetTrigger(animOpenDoor);
        photonView.RPC("RPC_PlayCloseDoorAnim", RpcTarget.Others, photonView.ViewID);
        Debug.Log("PlayOpenDoorAnim RPC sent");
    }

    [PunRPC]
    private void RPC_PlayOpenDoorAnim(int viewID)
    {
        if (photonView.ViewID == viewID)
        {
            animator.SetTrigger(animOpenDoor);
            animator.ResetTrigger(animCloseDoor);
            Debug.Log("PlayOpenDoorAnim RPC recieved");
        }
    }
    [PunRPC]
    private void RPC_PlayCloseDoorAnim(int viewID)
    {
        if (photonView.ViewID == viewID)
        {
            animator.SetTrigger(animCloseDoor);
            animator.ResetTrigger(animOpenDoor);
            Debug.Log("PlayCloseDoorAnim RPC recieved");
        }
    }
}
