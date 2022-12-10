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

    private PhotonView photonView;

    private int animIDOpen;
    private int animIDClose;

    private void Awake()
    {

    }
}
