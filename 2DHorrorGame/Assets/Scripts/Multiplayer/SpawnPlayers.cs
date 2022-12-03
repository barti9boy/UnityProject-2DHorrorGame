using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System;

public class SpawnPlayers : MonoBehaviour
{
    /*-----Player spawn variables-----*/
    public GameObject playerPrefab;
    private GameObject player;
    public float minX;
    public float maxX;
    public event EventHandler<GameObject> OnPlayerLoaded;

    /*-----UI elements-----*/
    public GameObject UIPrefab;
    public Image itemImage0;
    public Image itemImage1;
    public Image itemImage2;


    private void Awake()
    {
        Vector2 randomSpawnPosition = new Vector2(UnityEngine.Random.Range(minX, maxX), -1.64554f); // for now Y is hard coded, change 
        player = PhotonNetwork.Instantiate(playerPrefab.name, randomSpawnPosition, Quaternion.identity);
        player.GetComponent<PlayerInventory>().inventoryItems[0] = itemImage0;
        player.GetComponent<PlayerInventory>().inventoryItems[1] = itemImage1;
        player.GetComponent<PlayerInventory>().inventoryItems[2] = itemImage2;
    }
    private void Start()
    {
        OnPlayerLoaded?.Invoke(this, player);
    }
}
