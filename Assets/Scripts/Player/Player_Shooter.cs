using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shooter : MonoBehaviour
{
    [SerializeField] private AudioClip shootClip;
    private Main_Controller main_Controller;

    public GameObject laserPrefab;

    private Animator anim;

    private void Start()
    {
        main_Controller = GameObject.FindGameObjectWithTag("Main_Controller").GetComponent<Main_Controller>();
        anim = this.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && main_Controller.IsPlayerAlive() == true && main_Controller.GameIsPaused() == false && main_Controller.GameStarted() == true)
        {
            Instantiate(laserPrefab, transform.position, transform.rotation);
            main_Controller.EffectsPlay(shootClip);
            anim.SetTrigger("Squash");
        }
    }
}
