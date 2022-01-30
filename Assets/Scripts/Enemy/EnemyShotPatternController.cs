using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyShotPatternController : MonoBehaviour
{
    //defines the shot type
    public enum shotPattern // your custom enumeration
    {
        bottom,             // shots to the bottom only
        atPlayer,           // shots at player
        nova3Way,           // bottom, bottom right, bottom left
        nova5Way,           // 3 way + left and right
        nova8Way,           // all directions
        homing              // chases player       
    };
    public shotPattern currentPattern;// = shotPattern.bottom;

    public GameObject shotObject;
    public EnemyMoveControl enemyMoveControl;

    public float shotDelay;
    public float initialDelay;
    bool canShot;

    private void Awake()
    {
        currentPattern = (shotPattern)Random.Range(0, 5);
    }
    // Start is called before the first frame update
    void Start()
    {
        canShot = true;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (canShot) StartCoroutine(startShot());
    }

    IEnumerator startShot()
    {
        canShot = false;
        Debug.Log("Shot!");
        shot();
        yield return new WaitForSeconds(shotDelay);
        canShot = true;
    }

    public void shot()
    {
        switch(currentPattern)
        {
            case shotPattern.atPlayer:
                shotAtPlayer();
                break;
            case shotPattern.bottom:
                shotBottom();
                break;
            case shotPattern.nova3Way:
                shot3Way();
                break;
            case shotPattern.nova5Way:
                shot5Way();
                break;
            case shotPattern.nova8Way:
                shot8Way();
                break;
        }
    }

    public void shotBottom()
    {
        GameObject enemyShoot = PhotonNetwork.Instantiate("enemyShot", transform.position, Quaternion.identity);

        EnemyShotController temp = enemyShoot.GetComponent<EnemyShotController>();

        if (enemyMoveControl.polarity == EnemyMoveControl.Polarity.black)
        {
            temp.polarity = EnemyShotController.Polarity.black;
        }
        else
        {
            temp.polarity = EnemyShotController.Polarity.white;
        }

        temp.currectDirection = EnemyShotController.direction.bottom;
    }

    public void shot3Way()
    {
        //bottom shot
        GameObject enemyShoot = PhotonNetwork.Instantiate("enemyShot", transform.position, Quaternion.identity);
        EnemyShotController temp = enemyShoot.GetComponent<EnemyShotController>();
        if (enemyMoveControl.polarity == EnemyMoveControl.Polarity.black)
        {
            temp.polarity = EnemyShotController.Polarity.black;
        }
        else
        {
            temp.polarity = EnemyShotController.Polarity.white;
        }
        temp.currectDirection = EnemyShotController.direction.bottom;

        //bottomLeftshot
        enemyShoot = PhotonNetwork.Instantiate("enemyShot", transform.position, Quaternion.identity);
        temp = enemyShoot.GetComponent<EnemyShotController>();
        if (enemyMoveControl.polarity == EnemyMoveControl.Polarity.black)
        {
            temp.polarity = EnemyShotController.Polarity.black;
        }
        else
        {
            temp.polarity = EnemyShotController.Polarity.white;
        }
        temp.currectDirection = EnemyShotController.direction.bottomleft;

        //bottomRightshot
        enemyShoot = PhotonNetwork.Instantiate("enemyShot", transform.position, Quaternion.identity);
        temp = enemyShoot.GetComponent<EnemyShotController>();
        if (enemyMoveControl.polarity == EnemyMoveControl.Polarity.black)
        {
            temp.polarity = EnemyShotController.Polarity.black;
        }
        else
        {
            temp.polarity = EnemyShotController.Polarity.white;
        }
        temp.currectDirection = EnemyShotController.direction.bottomRight;
    }

    public void shot5Way()
    {
        shot3Way();

        //bottom shot
        GameObject enemyShoot = PhotonNetwork.Instantiate("enemyShot", transform.position, Quaternion.identity);
        EnemyShotController temp = enemyShoot.GetComponent<EnemyShotController>();
        if (enemyMoveControl.polarity == EnemyMoveControl.Polarity.black)
        {
            temp.polarity = EnemyShotController.Polarity.black;
        }
        else
        {
            temp.polarity = EnemyShotController.Polarity.white;
        }
        temp.currectDirection = EnemyShotController.direction.left;

        //bottomLeftshot
        enemyShoot = PhotonNetwork.Instantiate("enemyShot", transform.position, Quaternion.identity);
        temp = enemyShoot.GetComponent<EnemyShotController>();
        if (enemyMoveControl.polarity == EnemyMoveControl.Polarity.black)
        {
            temp.polarity = EnemyShotController.Polarity.black;
        }
        else
        {
            temp.polarity = EnemyShotController.Polarity.white;
        }
        temp.currectDirection = EnemyShotController.direction.right;
    }

    public void shot8Way()
    {
        shot5Way();

        //topRightShot
        GameObject enemyShoot = PhotonNetwork.Instantiate("enemyShot", transform.position, Quaternion.identity);
        EnemyShotController temp = enemyShoot.GetComponent<EnemyShotController>();
        if (enemyMoveControl.polarity == EnemyMoveControl.Polarity.black)
        {
            temp.polarity = EnemyShotController.Polarity.black;
        }
        else
        {
            temp.polarity = EnemyShotController.Polarity.white;
        }
        temp.currectDirection = EnemyShotController.direction.topRight;

        //topLeftShot
        enemyShoot = PhotonNetwork.Instantiate("enemyShot", transform.position, Quaternion.identity);
        temp = enemyShoot.GetComponent<EnemyShotController>();
        if (enemyMoveControl.polarity == EnemyMoveControl.Polarity.black)
        {
            temp.polarity = EnemyShotController.Polarity.black;
        }
        else
        {
            temp.polarity = EnemyShotController.Polarity.white;
        }
        temp.currectDirection = EnemyShotController.direction.topLeft;

        //topShot
        enemyShoot = PhotonNetwork.Instantiate("enemyShot", transform.position, Quaternion.identity);
        temp = enemyShoot.GetComponent<EnemyShotController>();
        if (enemyMoveControl.polarity == EnemyMoveControl.Polarity.black)
        {
            temp.polarity = EnemyShotController.Polarity.black;
        }
        else
        {
            temp.polarity = EnemyShotController.Polarity.white;
        }
        temp.currectDirection = EnemyShotController.direction.top;
    }

    public void shotAtPlayer()
    {
        //topRightShot
        GameObject enemyShoot = PhotonNetwork.Instantiate("enemyShot", transform.position, Quaternion.identity);
        EnemyShotController temp = enemyShoot.GetComponent<EnemyShotController>();
        if (enemyMoveControl.polarity == EnemyMoveControl.Polarity.black)
        {
            temp.polarity = EnemyShotController.Polarity.black;
        }
        else
        {
            temp.polarity = EnemyShotController.Polarity.white;
        }
        temp.currectDirection = EnemyShotController.direction.onPlayer;
    }

}
