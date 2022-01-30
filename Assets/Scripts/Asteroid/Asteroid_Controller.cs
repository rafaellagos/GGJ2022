﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Asteroid_Controller : MonoBehaviour
{
    private enum AsteroidSize { small, medium, large };
    [SerializeField] AsteroidSize asteroid;
    [SerializeField] int asteroidLife;
    [SerializeField] float timeToCollision;
    [SerializeField] float impact;

    private Main_Controller mainController;
    private bool canMove;
    private Animator anim;
    private Collider2D myCollider;
    private int direction;
    private bool canColid;
    private Rigidbody2D myRigidbody;
    [SerializeField] private int asteroidValue;
    [SerializeField] private float scrollingSpeed;
    [SerializeField] private AudioClip explosionEffect;


    public PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        mainController = GameObject.FindGameObjectWithTag("Main_Controller").GetComponent<Main_Controller>();

        anim = this.GetComponent<Animator>();
        myCollider = this.GetComponent<Collider2D>();
        myRigidbody = this.GetComponent<Rigidbody2D>();

        canMove = true;
        canColid = true;

        view = GetComponent<PhotonView>();
    }

    void FixedUpdate()
    {
        if (mainController.gameEnded == true)
        {
            anim.SetTrigger("Explosion");
        }
        if (canMove == true)
        {
            if (asteroid == AsteroidSize.large)
            {
                transform.Translate(Vector2.down * scrollingSpeed * Time.deltaTime);
            }
            if (asteroid == AsteroidSize.medium)
            {
                transform.Translate(Vector2.down * scrollingSpeed * Time.deltaTime);
                if (direction == 0)
                {
                    transform.Translate(Vector2.left * scrollingSpeed * Time.deltaTime);
                }
                if (direction == 1)
                {
                    transform.Translate(Vector2.right * scrollingSpeed * Time.deltaTime);
                }
            }
            if (asteroid == AsteroidSize.small)
            {
                if (direction == 0)
                {
                    transform.Translate(Vector2.left * scrollingSpeed * Time.deltaTime);
                }
                if (direction == 1)
                {
                    transform.Translate(Vector2.right * scrollingSpeed * Time.deltaTime);
                }
            }
        }
        
    }



    public void AsteroidCollision(Transform hitObj) 
    {
        if (canColid == true)
        {
            canColid = false;
            mainController.EffectsPlay(explosionEffect);
           
            mainController.ScreenShake();
            Vector3 moveDirection = hitObj.transform.position - this.transform.position;
            myRigidbody.AddForce(moveDirection.normalized * impact);
            asteroidLife -= 1;
            anim.SetTrigger("Damage");
            anim.SetTrigger("Squash");
            StartCoroutine("CanCollid");
            if (asteroidLife <= 0)
            {

                mainController.EnemyKill();
                mainController.AddPoints(asteroidValue);
                anim.SetTrigger("Explosion");
                myCollider.enabled = false;
                canMove = false;
                if (asteroid == AsteroidSize.large)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Asteroid_Controller prefab = PhotonNetwork.Instantiate("Asteroid Medium", transform.position, transform.rotation).GetComponent<Asteroid_Controller>();

                        prefab.direction = i;
                    }
                }
                if (asteroid == AsteroidSize.medium)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Asteroid_Controller prefab = PhotonNetwork.Instantiate("Asteroid Small", transform.position, transform.rotation).GetComponent<Asteroid_Controller>();
                        prefab.direction = i;
                    }
                }

            }


        } 
    }

    IEnumerator CanCollid() 
    {
        yield return new WaitForSeconds(timeToCollision);
        canColid = true;
    }

    public void CollisionCall(Transform collisionPoint)
    {
        AsteroidCollision(collisionPoint);
    }

    public void DestroyObj() 
    {
        Destroy();
    }

    public void Destroy()
    {
       view.RPC("CallDestroy", RpcTarget.All);
    }

    #region ONLINE

    [PunRPC]
    public void CallDestroy()
    {
        Destroy(this.gameObject);
    }

    #endregion

}
