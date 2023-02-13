using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManagerScript : MonoBehaviourPunCallbacks
{
    public SpawnPlayers spawnPlayersScript;
    public event EventHandler<GameObject> OnAfterPlayerLoaded;
    private GameObject player;
    bool m_IsPlaying = false;

    private void Awake()
    {
        spawnPlayersScript = transform.GetComponentInChildren<SpawnPlayers>();
        spawnPlayersScript.OnPlayerLoaded += SpawnPlayersScript_OnPlayerLoaded;
    }
    void Update()
    {
        if (AudioManager.Instance != null && !m_IsPlaying)
        {
            AudioManager.Instance.PlayAmbient(Clip.widnAmbient);
            m_IsPlaying = true;
        }
    }
    private void SpawnPlayersScript_OnPlayerLoaded(object sender, GameObject e)
    {
        Debug.Log("a player loaded into the level");
        player = e;
        OnAfterPlayerLoaded?.Invoke(this, player);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("MainMenu");
    }
}
