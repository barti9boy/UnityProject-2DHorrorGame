using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public SpawnPlayers spawnPlayersScript;
    public event EventHandler<GameObject> OnAfterPlayerLoaded;
    private GameObject player;

    private void Awake()
    {
        spawnPlayersScript = transform.GetComponentInChildren<SpawnPlayers>();
        spawnPlayersScript.OnPlayerLoaded += SpawnPlayersScript_OnPlayerLoaded;
    }
    private void Start()
    {
    }
    private void SpawnPlayersScript_OnPlayerLoaded(object sender, GameObject e)
    {
        Debug.Log("a player loaded into the level");
        player = e;
        OnAfterPlayerLoaded?.Invoke(this, player);
    }
}
