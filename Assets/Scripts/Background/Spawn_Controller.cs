using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawn_Controller : MonoBehaviour
{
    
    [SerializeField] private int baseEnemies;
    [SerializeField] private int currentEnemies;
    [SerializeField] private int currentWave;
    [SerializeField] private bool canSpawn;

    private Main_Controller main_Controller;

    // Start is called before the first frame update
    void Start()
    {
        main_Controller = GameObject.FindGameObjectWithTag("Main_Controller").GetComponent<Main_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn == true)
        {
            Spawn();
            canSpawn = false;
        }
        if (currentEnemies <= 0)
        {
            main_Controller.destroyLasers = true;
            canSpawn = true;
            currentWave++;
        }
    }

    private void Spawn() 
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < baseEnemies + currentWave; i++)
            {
                Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));

                PhotonNetwork.Instantiate("Asteroid Large", randomPositionOnScreen, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
                currentEnemies += 7;
            }
            main_Controller.DestroyLasers();
            main_Controller.ResetInvencibility();
        }
        
    }

    public void DecreaseEnemies() 
    {
        currentEnemies -= 1;
    }

    public void ResetGame()
    {
        currentEnemies = 0;
        currentWave = 0;
    }


}
