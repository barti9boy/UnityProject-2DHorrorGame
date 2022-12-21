using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
public class PressurePlateScript : MonoBehaviour
{
    public event EventHandler OnPlayerStand;
    public event EventHandler OnPlayerLeave;

    private PhotonView photonView;
    private Animator animator;
    private int animIDOn;
    private int animIDOff;
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        AssignAnimationIDs();
    }
    private void AssignAnimationIDs()
    {
        animIDOn = Animator.StringToHash("TriggerOn");
        animIDOff = Animator.StringToHash("TriggerOff");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            photonView.RPC("RPC_OnPlayerStand", RpcTarget.All, photonView.ViewID);
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            photonView.RPC("RPC_OnPlayerLeave", RpcTarget.All, photonView.ViewID);
        }
    }
    [PunRPC]
    private void RPC_OnPlayerStand(int viewID)
    {
        if (photonView.ViewID == viewID)
        {
            animator.ResetTrigger(animIDOff);
            animator.SetTrigger(animIDOn);
            OnPlayerStand?.Invoke(this, EventArgs.Empty);
        }
    }
    [PunRPC]
    private void RPC_OnPlayerLeave(int viewID)
    {
        if (photonView.ViewID == viewID)
        {
            animator.ResetTrigger(animIDOn);
            animator.SetTrigger(animIDOff);
            OnPlayerLeave?.Invoke(this, EventArgs.Empty);
        }
    }
}
