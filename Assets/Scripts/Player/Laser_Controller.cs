using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Laser_Controller : MonoBehaviour
{
    [SerializeField] private float scrollingSpeed;
    [SerializeField] private float destroyTime;
    [SerializeField] private float explosionTime;

    private Main_Controller mainController;
    private Animator anim;
    private bool canMove;
    private Collider2D myCollider;
    private bool canCollide;

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("spawned");

        mainController = GameObject.FindGameObjectWithTag("Main_Controller").GetComponent<Main_Controller>();
        anim = this.GetComponent<Animator>();
        myCollider = this.GetComponent<Collider2D>();
        canMove = true;
        canCollide = true;
        Invoke("Destroy", destroyTime);
    }

    void FixedUpdate()
    {
       /* if (mainController.destroyLasers == true)
        {
            Destroy();
            canCollide = false;
            myCollider.enabled = false;
        }*/
        if (canMove == true)
        {
            transform.Translate(Vector2.up * scrollingSpeed * Time.deltaTime);
        }
    }

    IEnumerator StopMovement() 
    {     
        yield return new WaitForSeconds(explosionTime);
        anim.SetTrigger("Explosion");
        canMove = false;
    }

    public void Destroy() 
    {   
        Destroy(this.gameObject);
        Debug.Log("destroy");


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Asteroid") && mainController.gameStarted && canCollide == true)
        {
            canCollide = false;
            myCollider.enabled = false;
            StartCoroutine("StopMovement");
            collision.GetComponent<Asteroid_Controller>().CollisionCall(this.transform);
        }

    }
}
