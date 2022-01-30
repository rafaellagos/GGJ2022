using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Player_Life_Controller : MonoBehaviour
{

    [SerializeField] private int playerLife;
  //  [SerializeField] private Text playerLifeText;
    [SerializeField] private AudioClip damageEffect;
    [SerializeField] private AudioClip destroyEffect;
    [SerializeField] private float invencibleTime;

    private Main_Controller main_Controller;
    private Animator anim;
    private Collider2D myCollider;
    private bool canTakeDamage;

    public PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        main_Controller = GameObject.FindGameObjectWithTag("Main_Controller").GetComponent<Main_Controller>();
        anim = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();

        if (PhotonNetwork.IsMasterClient)
        {
            main_Controller.view.RPC("SetP1Life", RpcTarget.All, 3);
        }
        else
        {
            main_Controller.view.RPC("SetP2Life", RpcTarget.All, 3);
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
        if (collision.tag.Equals("Asteroid") && main_Controller.gameStarted)
        {
            if (playerLife > 0 && canTakeDamage == true)
            {
                playerLife--;

                if (PhotonNetwork.IsMasterClient)
                {
                    main_Controller.view.RPC("SetP1Life", RpcTarget.All, playerLife);

                }
                else
                {
                    main_Controller.view.RPC("SetP2Life", RpcTarget.All, playerLife);
                }


                main_Controller.view.RPC("SetScreenShake", RpcTarget.All, playerLife);


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
            }
        }       
    }
}
