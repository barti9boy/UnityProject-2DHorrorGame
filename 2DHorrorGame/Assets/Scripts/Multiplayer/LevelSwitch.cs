using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LevelSwitch : MonoBehaviourPunCallbacks
{

    private PhotonView photonView;
    [SerializeField] private string levelName;
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            photonView.RPC("RPC_OnPlayerEnter", RpcTarget.All, photonView.ViewID);
        }
    }

    [PunRPC]
    private void RPC_OnPlayerEnter(int viewID)
    {
        if (photonView.ViewID == viewID)
        {
            PhotonNetwork.LoadLevel(levelName);
        }
    }
}
