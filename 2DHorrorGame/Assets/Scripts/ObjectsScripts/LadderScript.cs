using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public class LadderScript : MonoBehaviour
{
    public bool isEntrance;
    [SerializeField] public Transform DownPoint;
    [SerializeField] public Transform UpPoint;
    public AnimationClip usingVentEntraceAnimation;
    public AnimationClip usingVentExitAnimation;
    public AnimationClip ventEntranceAnimation;


    private Animator animator;
    private PhotonView photonView;

    private int animIDOpen;
    private int animIDClose;

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
        animIDOpen = Animator.StringToHash("TriggerOpen");
        //animIDClose = Animator.StringToHash("TriggerClose");
    }

    public void PlayOpenAnim()
    {
        animator.SetTrigger(animIDOpen);
        //animator.ResetTrigger(animIDClose);
        photonView.RPC("RPC_PlayOpenAnim", RpcTarget.Others, photonView.ViewID);
        Debug.Log("PlayOpenAnim RPC sent");
        CoroutineHandler.Instance.WaitUntilAnimated(ventEntranceAnimation.length, () => ResetTrigger());

    }

    private void ResetTrigger()
    {
        animator.ResetTrigger(animIDOpen);
        photonView.RPC("RPC_ResetTrigger", RpcTarget.Others, photonView.ViewID);
        Debug.Log("ResetTrigger RPC sent");
    }

    //public void PlayCloseAnim()
    //{
    //    animator.SetTrigger(animIDOpen);
    //    animator.ResetTrigger(animIDClose);
    //    photonView.RPC("RPC_PlayCloseAnim", RpcTarget.Others, photonView.ViewID);
    //    Debug.Log("PlayClosenim RPC sent");
    //}
    [PunRPC]
    private void RPC_PlayOpenAnim(int viewID)
    {
        if (photonView.ViewID == viewID)
        {
            animator.SetTrigger(animIDOpen);
            //animator.ResetTrigger(animIDClose);
            Debug.Log("PlayOpenAnim RPC recieved");
        }
    }

    [PunRPC]
    private void RPC_ResetTrigger(int viewID) 
    {
        if (photonView.ViewID == viewID)
        {
            animator.ResetTrigger(animIDOpen);
            //animator.ResetTrigger(animIDClose);
            Debug.Log("ResetTrigger RPC recieved");
        }
    }

    //[PunRPC]
    //private void RPC_PlayCloseAnim(int viewID)
    //{
    //    if (photonView.ViewID == viewID)
    //    {
    //        animator.SetTrigger(animIDOpen);
    //        animator.ResetTrigger(animIDClose);
    //        Debug.Log("PlayCloseAnim RPC recieved");
    //    }
    //}
}
