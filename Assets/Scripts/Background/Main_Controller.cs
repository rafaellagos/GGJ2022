using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;
using Photon.Pun;

public class Main_Controller : MonoBehaviour
{


    public bool gameStarted;
    public bool destroyLasers;
    public bool playerAlive;
    public bool gameEnded;

    public GameObject player;
    public Spawn_Controller spawn;

    [Header("Prefabs for asteroids")]
    [Tooltip("0 - large / 1 - medium / 2 - small")]
    public Asteroid_Controller[] asteroidPrefabs;


    [SerializeField] private UI_Controller uI_Controller;
    [SerializeField] private Text startText;
    [SerializeField] private Text tutorialText;
    [SerializeField] private Text pointsText;
    [SerializeField] private AudioClip startSound;
    [SerializeField] private AudioSource effects;

    private string playAgainString = "press enter to play again";
    private Player_Life_Controller player_Life_Controller;
    private Player_Movement player_Movement;
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


    void Start()
    {
        playerAlive = true;
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (player == null)player =  GameObject.FindGameObjectWithTag("Player");
        if (player_Life_Controller == null) player_Life_Controller = player.GetComponent<Player_Life_Controller>();
        if (player_Movement == null) player_Movement = player.GetComponent<Player_Movement>();


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
                    player_Life_Controller.ResetGame();
                    player_Movement.ResetPlayerOrientation();
                    points = 0;
                    pointsText.text = points.ToString();
                    gameEnded = false;
                    playerAlive = true;
                }
            }
        }


        GameIsPaused();



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
        player_Life_Controller.Invencibility();
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
    public void ScreenShake( ) 
    {
         tempMagnitude = (float)(Random.Range(0, 40)) / 10;
         tempRoughness = (float)(Random.Range(0, 40)) / 10;
         tempFadeInTime = (float)(Random.Range(0, 10)) / 10;
         tempFadeOutTime = (float)(Random.Range(1, 10)) / 10;

        CameraShaker.Instance.ShakeOnce(tempMagnitude, tempRoughness, tempFadeInTime, tempFadeOutTime);
    }



    #region ONLINE
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

    #endregion
}
