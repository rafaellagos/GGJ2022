using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class SpawnPlayers : MonoBehaviour
{

    public GameObject player1Prefab;
    public GameObject player2Prefab;

    public GameObject prefabBullets;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;



    void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(player1Prefab.name, randomPosition, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate(player2Prefab.name, randomPosition, Quaternion.identity);
        }
        

    }

    void Update()
    {
        
    }

    public void InstantiateBullet(Vector2 bulletPosition) 
    {
        PhotonNetwork.Instantiate(prefabBullets.name, bulletPosition, Quaternion.identity);
    }
}
