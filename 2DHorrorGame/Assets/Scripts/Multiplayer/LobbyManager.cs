using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{

    public TMP_InputField createRoomInput;

    public void CreateRoom()
    {
        PhotonNetwork.JoinOrCreateRoom(createRoomInput.text, new RoomOptions { MaxPlayers = 2 }, null);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("TutorialFloor");
    }
}
