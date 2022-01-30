using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemiesSpawn_Manager : MonoBehaviour
{
    public string[] enemies;
    public string boss;

    public float minTime;
    public float maxTime;

    public int enemySpawnCount;
    public int bossSpawnRequirement;


    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Invoke(nameof(SpawnSystem), GetTimeForNextSpawn());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnSystem() 
    {
        if (enemySpawnCount < bossSpawnRequirement)
        {
            SpawnEnemy();
        }
        else
        {
            SpawnBoss();
        }

        Invoke(nameof(SpawnSystem), GetTimeForNextSpawn());
    }


    public Vector2 GetRandomPosition() 
    {
        Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
        return randomPositionOnScreen;
    }

    public float GetTimeForNextSpawn() 
    {
        float nextSpawn = Random.Range(minTime, maxTime);
        return nextSpawn;
    }

    public void SpawnEnemy()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int randomEnemy = Random.Range(0, enemies.Length);
            PhotonNetwork.Instantiate(enemies[randomEnemy], GetRandomPosition(), Quaternion.identity);
        }
    }

    public void SpawnBoss()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(boss, GetRandomPosition(), Quaternion.identity);
        }
    }
}
