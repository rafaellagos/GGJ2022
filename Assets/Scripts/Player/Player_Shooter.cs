using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_Shooter : MonoBehaviour
{
    [SerializeField] private AudioClip shootClip;
    private Main_Controller main_Controller;

    public GameObject laserPrefab;

    private Animator anim;

    public PhotonView view;

    private void Start()
    {
        main_Controller = GameObject.FindGameObjectWithTag("Main_Controller").GetComponent<Main_Controller>();
        anim = this.GetComponent<Animator>();
            view = GetComponent<PhotonView>();

    }
    // Update is called once per frame
    void Update()
    {

        if (view.IsMine)
        {
            if (Input.GetButtonDown("Fire1") && main_Controller.IsPlayerAlive() == true && main_Controller.GameIsPaused() == false && main_Controller.GameStarted() == true)
            {
                main_Controller.spawnPlayers.InstantiateBullet(transform.position);
                //Instantiate(laserPrefab, transform.position, transform.rotation);
                main_Controller.EffectsPlay(shootClip);
                anim.SetTrigger("Squash");
            }
        }
    }
}
