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
        LadderPosition = 3,
        VentPosition = 4
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
        InputManager.OnFlashlightButtonClicked += TurnFlashlightOnOff;
    }

    private void OnDestroy()
    {
        InputManager.OnFlashlightButtonClicked -= TurnFlashlightOnOff;
    }

    public void TurnFlashlightOnOff(bool isOn)
    {
        if(photonView.IsMine)
        {
            Debug.Log($"Flashlight {isOn}");
            gameObject.SetActive(isOn);
            photonView.RPC("RPC_TurnFlashlightOnOff", RpcTarget.Others, photonView.ViewID, isOn);
        }
    }



    [PunRPC]
    private void RPC_TurnFlashlightOnOff(int viewId, bool isOn)
    {
        if (photonView.ViewID == viewId)
        {
            gameObject.SetActive(isOn);
        }
    }
}
