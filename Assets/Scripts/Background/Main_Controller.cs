using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class Main_Controller : MonoBehaviour
{


    public bool gameStarted;
    public bool destroyLasers;
    public bool playerAlive;
    public bool gameEnded;

    public Spawn_Controller spawn;

   /* [Header("Prefabs for asteroids")]
    [Tooltip("0 - large / 1 - medium / 2 - small")]
    public Asteroid_Controller[] asteroidPrefabs;*/


    [SerializeField] private UI_Controller uI_Controller;
    [SerializeField] private Text startText;
    [SerializeField] private Text tutorialText;
    [SerializeField] private Text pointsText;
    [SerializeField] private AudioClip startSound;
    [SerializeField] private AudioSource effects;

    private string playAgainString = "Match Ended";
    
    private int points;
    private bool startGame;

    [SerializeField] float tempMagnitude;
    [SerializeField] float tempRoughness;
    [SerializeField] float tempFadeInTime;
    [SerializeField] float tempFadeOutTime;

    public SpawnPlayers spawnPlayers;



    public PhotonView view;

    public Text player1Life;
    public Text player2Life;

    public int p1Life;
    public int p2Life;

    public GameObject gameOVerPanel;

    public GameObject player1;
    public GameObject player2;

    public Player_Life_Controller p1LifeController;
    public Player_Life_Controller p2LifeController;
    public Player_Movement p1Movement;
    public Player_Movement p2Movement;


    public void SetVariablesPlayer1()
    {
        if (gameEnded == false && GameIsPaused() == false)
        {
            player1 = GameObject.Find("Player1(Clone)");
            p1LifeController = player1.GetComponent<Player_Life_Controller>();
            p1Movement = player1.GetComponent<Player_Movement>();
        }
    }

    public void SetVariablesPlayer2()
    {
       
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1 && gameEnded == false)
        {
            player2 = GameObject.Find("Player2(Clone)");
            p2LifeController = player2.GetComponent<Player_Life_Controller>();
            p2Movement = player2.GetComponent<Player_Movement>();
        }   
           
    }

   

    void Start()
    {
        playerAlive = true;
        view = GetComponent<PhotonView>();


        p1Life = 3;

        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            p2Life = 3;
        }
    }

    

    void Update()
    {

        if (player1 == null || p1LifeController == null || p1Movement == null)
        {
            SetVariablesPlayer1();
        }
        if (player2 == null || p2LifeController == null || p2Movement == null)
        {
            SetVariablesPlayer2();
        }

        if (gameEnded == false)
        {
          /*  if (player == null) player = GameObject.FindGameObjectWithTag("Player");
            if (player_Life_Controller == null) player_Life_Controller = player.GetComponent<Player_Life_Controller>();
            if (player_Movement == null) player_Movement = player.GetComponent<Player_Movement>();*/
        }
      


        if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel"))
        {
            if (gameStarted == false && startGame == false)
            {
                startGame = true;
                startText.gameObject.SetActive(false);
                tutorialText.gameObject.SetActive(false);

                effects.PlayOneShot(startSound);
                StartCoroutine("GameStart");
                if (playerAlive == true)
                {
                    ResetInvencibility();
                }
                else
                {
                    spawn.ResetGame();
                  //  player_Life_Controller.ResetGame();
                    //player_Movement.ResetPlayerOrientation();
                    points = 0;
                    pointsText.text = points.ToString();
                    gameEnded = false;
                    playerAlive = true;
                }
            }
        }


        GameIsPaused();

        if (p1Life <= 0 && p2Life <= 0)
        {
            gameEnded = true;
            gameOVerPanel.SetActive(true);
        }

        if (gameStarted)
        {
            if (p1Life <= 0)
            {
                P1Death();
            }
            if (p2Life <= 0)
            {
                P2Death();
            }
        }

    }

    public void P1Death()
    {
        player1.GetComponent<Player_Shooter>().enabled = false;
        p1LifeController.enabled = false;
        p1Movement.enabled = false;
    }

    public void P2Death()
    {

        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            player2.GetComponent<Player_Shooter>().enabled = false;
            p2LifeController.enabled = false;
            p2Movement.enabled = false;
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }

    public void GameOver()
    {
        view.RPC("CallGameOver", RpcTarget.All);
    }


    IEnumerator Disconnect()
    {
        /*if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }*/


        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
        {
            yield return null;
            Debug.Log("Disconnecting. . .");
        }
        Debug.Log("DISCONNECTED!");


        PhotonNetwork.LoadLevel(0);
    }


    IEnumerator GameStart() 
    {
        yield return new WaitForSeconds(0.5f);
        gameStarted = true;
        startGame = false;
    } 
    public void PlayerDeath() 
    {
        gameStarted = false;
        startText.text = playAgainString;
        startText.gameObject.SetActive(true);
        tutorialText.gameObject.SetActive(true);
        playerAlive = false;
        gameEnded = true;
    }

    public void EnemyKill() 
    {
        spawn.DecreaseEnemies();
    }

    public void AddPoints(int addPoints) 
    {
        points += addPoints;
        pointsText.text = points.ToString();
    }

    public void EffectsPlay(AudioClip clip) 
    {
        effects.PlayOneShot(clip);
    }

    public void DestroyLasers() 
    {
        destroyLasers = false;
    }

    public void ResetInvencibility() 
    {
        if (p1LifeController != null) p1LifeController.Invencibility();
        if (p2LifeController != null) p2LifeController.Invencibility();
    }

    public bool GameIsPaused() 
    {     
       return uI_Controller.gameIsPaused;
    } 
    public bool GameStarted() 
    {     
       return gameStarted;
    }
    public bool GameEnded() 
    {     
       return gameEnded;
    } 
    public bool IsPlayerAlive() 
    {     
       return playerAlive;
    }
    public void ScreenShake() 
    {
         tempMagnitude = (float)(Random.Range(0, 40)) / 10;
         tempRoughness = (float)(Random.Range(0, 40)) / 10;
         tempFadeInTime = (float)(Random.Range(0, 10)) / 10;
         tempFadeOutTime = (float)(Random.Range(1, 10)) / 10;

        CameraShaker.Instance.ShakeOnce(tempMagnitude, tempRoughness, tempFadeInTime, tempFadeOutTime);
    }


    
    public void UpdateP1Lives() 
    {
        view.RPC("SetP1Life", RpcTarget.All, p1Life);
        Debug.Log("P1 --");
    }
    
    public void UpdateP2Lives() 
    {
        view.RPC("SetP2Life", RpcTarget.All, p2Life);
        Debug.Log("P2 --");

    }
    #region ONLINE

    [PunRPC]
    public void SetP1Life(int life)
    {
        player1Life.text = life.ToString();
    }

    [PunRPC]
    public void SetP2Life(int life)
    {
        player2Life.text = life.ToString();
    }

    [PunRPC]
    public void SetScreenShake()
    {
        ScreenShake();
    }
    
    [PunRPC]
    public void CallGameOver()
    {
        StartCoroutine(Disconnect());
    }



    /*   if (Input.GetKeyDown(KeyCode.O))
        {
            p1Life++;
            view.RPC("SetP1Life", RpcTarget.All, p1Life);
        }
if (Input.GetKeyDown(KeyCode.P))
{
    p2Life++;
    view.RPC("SetP2Life", RpcTarget.All, p2Life);
}*/
    #endregion
}
