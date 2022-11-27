using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public float minX;
    public float maxX;

    private void Start()
    {
        Vector2 randomSpawnPosition = new Vector2(Random.Range(minX, maxX), -1.64554f); // for now Y is hard coded, change 
        PhotonNetwork.Instantiate(playerPrefab.name, randomSpawnPosition, Quaternion.identity);
    }
}
