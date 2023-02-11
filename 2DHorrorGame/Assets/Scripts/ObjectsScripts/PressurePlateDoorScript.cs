using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public class PressurePlateDoorScript : MonoBehaviour, IInteractible
{
    [SerializeField] private List<PressurePlateScript> pressurePlates = new List<PressurePlateScript>();
    private Collider2D collider;
    public int standingPlayerCount;
    public bool isOpened;

    private Animator animator;
    private int animOpenDoor;
    private int animCloseDoor;

    private PhotonView photonView;

    public GameObject interactionHighlight;
    //private InteractionHighlight highlight;
    //private readonly string closedText = "STAND ON A PRESSURE PLATE TO OPEN";
    //private readonly string openedText = "";

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
        //highlight = GetComponentInChildren<InteractionHighlight>();
        isOpened = false;
        standingPlayerCount = 0;
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
        standingPlayerCount--;
        if(standingPlayerCount == 0)
        {
            isOpened = false;
            collider.enabled = true;
            PlayCloseDoorAnim();
        }
    }

    private void Plate_OnPlayerStand(object sender, EventArgs e)
    {
        standingPlayerCount++;
        isOpened = true;
        collider.enabled = false;
        PlayOpenDoorAnim();
    }


    public void PlayOpenDoorAnim()
    {
        AudioManager.OnPlaySoundAtPosition(Clip.doorCreek, transform.position);
        animator.SetTrigger(animOpenDoor);
        animator.ResetTrigger(animCloseDoor);
        photonView.RPC("RPC_PlayOpenDoorAnim", RpcTarget.Others, photonView.ViewID);
        Debug.Log("PlayOpenDoorAnim RPC sent");
    }
    public void PlayCloseDoorAnim()
    {
        AudioManager.OnPlaySoundAtPosition(Clip.doorCreek, transform.position);
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
            AudioManager.OnPlaySoundAtPosition(Clip.doorCreek, transform.position);
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
            AudioManager.OnPlaySoundAtPosition(Clip.doorCreek, transform.position);
            animator.SetTrigger(animCloseDoor);
            animator.ResetTrigger(animOpenDoor);
            Debug.Log("PlayCloseDoorAnim RPC recieved");
        }
    }

    //public void ChangeInteractionCanvasTransform(float playerX, float doorX)
    //{
    //    Debug.Log("big bobas");
    //    if (playerX > doorX)
    //    {
    //        highlight.transform.localPosition = new Vector3(1.25f, 0, 0);
    //    }
    //    else
    //    {
    //        highlight.transform.localPosition = new Vector3(-1.25f, 0, 0);
    //    }
    //}
    //public void ChangeInteractionCanvasText()
    //{
    //    if (isOpened) highlight.InteractionText.text = openedText;
    //    else highlight.InteractionText.text = closedText;
    //}
    public void EnableInteractionHighlight()
    {
        interactionHighlight.SetActive(true);
    }

    public void DisableInteractionHighlight()
    {
        interactionHighlight.SetActive(false);
    }
}
