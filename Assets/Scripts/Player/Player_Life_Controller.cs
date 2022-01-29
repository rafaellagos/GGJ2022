using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
   
    // Start is called before the first frame update
    void Start()
    {
        main_Controller = GameObject.FindGameObjectWithTag("Main_Controller").GetComponent<Main_Controller>();
        anim = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
        Invoke("ActivateDamage", invencibleTime);
    }

    public void ActivateDamage()
    {
        canTakeDamage = true;
    }

    public void ResetGame()
    { 
        playerLife = 3;
      //  playerLifeText.text = "X " + playerLife.ToString();
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
                playerLife = playerLife - 1;
           //     playerLifeText.text = "X " + playerLife.ToString();     
                main_Controller.EffectsPlay(damageEffect);
                Invencibility();
                main_Controller.ScreenShake();
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
