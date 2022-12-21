using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FlashlightScript : MonoBehaviour
{
    public enum FlashlightPosition : byte
    {
        StandingPosition = 0,
        WalkingPosition = 1,
        SitingPosition = 2,
    }

    private PhotonView photonView;
    [SerializeField ]private List<Transform> flashlightPositions = new List<Transform>();

    public void ChangeFlashlightPosition(FlashlightPosition position)
    {
        transform.position = flashlightPositions[((byte)position)].position;
    }

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    public void TurnFlashlightOnOff(bool isOn)
    {
        gameObject.SetActive(isOn);
        if(photonView.IsMine)
            photonView.RPC("RPC_RemoveItemFromInventory", RpcTarget.Others, photonView.ViewID, isOn);
    }



    [PunRPC]
    private void RPC_RemoveItemFromInventory(int viewId, bool isOn)
    {
        if (photonView.ViewID == viewId)
        {
            TurnFlashlightOnOff(isOn);
        }
    }
}
