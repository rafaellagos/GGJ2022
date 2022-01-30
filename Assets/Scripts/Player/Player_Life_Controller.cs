using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Player_Life_Controller : MonoBehaviour
{

    [SerializeField] private int playerLife;
    [SerializeField] private AudioClip damageEffect;
    [SerializeField] private AudioClip destroyEffect;
    [SerializeField] private float invencibleTime;

    private Main_Controller main_Controller;
    private Animator anim;
    private Collider2D myCollider;
    private bool canTakeDamage;


    public Polarity polarity;
    public enum Polarity { white, black }


   
   
    void Start()
    {
        main_Controller = GameObject.FindGameObjectWithTag("Main_Controller").GetComponent<Main_Controller>();
        anim = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();

        if (PhotonNetwork.IsMasterClient)
        {
            main_Controller.view.RPC("SetP1Life", RpcTarget.All, 3);
            polarity = Polarity.white;
            transform.gameObject.tag = "White";
        }
        else
        {
            main_Controller.view.RPC("SetP2Life", RpcTarget.All, 3);
            polarity = Polarity.black;
            transform.gameObject.tag = "Black";
        }

        Invoke("ActivateDamage", invencibleTime);


    }


   
    public void ActivateDamage()
    {
        canTakeDamage = true;
    }

    public void ResetGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            main_Controller.view.RPC("SetP1Life", RpcTarget.All, 3);
        }
        else
        {
            main_Controller.view.RPC("SetP2Life", RpcTarget.All, 3);
        }
        myCollider.enabled = true;
        Invencibility();
    }

    public void Invencibility()
    {
        anim.SetTrigger("Invencibility");
        Invoke("ActivateDamage", invencibleTime);
        canTakeDamage = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


      /*  if ( collision.tag.Equals("White"))
        {
            if (polarity ==)
            {

            }
        }*/
        if (collision.tag.Equals("Asteroid")  && main_Controller.gameStarted)
        {
            if (playerLife > 0 && canTakeDamage == true)
            {
                Invencibility();
                Debug.Log("colidiu essa bosta");
                playerLife--;

              
                if (PhotonNetwork.IsMasterClient)
                {
                    Debug.Log("P1");
                    main_Controller.p1Life--;
                    main_Controller.UpdateP1Lives();
                }
                if (!PhotonNetwork.IsMasterClient)
                {
                    Debug.Log("P2");
                    main_Controller.p2Life--;
                    main_Controller.UpdateP2Lives();
                }

                main_Controller.view.RPC("SetScreenShake", RpcTarget.All);


                main_Controller.EffectsPlay(damageEffect);
                Invencibility();
               // main_Controller.ScreenShake();
                anim.SetTrigger("Damage");
            } 
            if (playerLife <= 0)
            {
                anim.SetTrigger("Death");
                myCollider.enabled = false;
                main_Controller.EffectsPlay(destroyEffect);
                main_Controller.PlayerDeath();

                PhotonNetwork.Destroy(this.gameObject);
            }
        }       
    }
}
