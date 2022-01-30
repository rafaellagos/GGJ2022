using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomingShotBehaviour : MonoBehaviour
{
    public Transform target;                    // target's transform (player)
    public Rigidbody2D rigidBody;               // my own rigid body
    public float angleChangingSpeed;            // how fast the shot will turn
    public float movementSpeed;                 // how fast it will travel  (controller will fill this)
    public float myLifeSpan;                    // how long it will last after activated  (controller will fill this)
    public int myDamage;                        // damage inflicted  (controller will fill this)
    public int myHp;                            // shot HP (it can be destroyed)
    public bool shotEnable;                     // if it can shot at the player
    public enum Polarity { white, black }       // polarity options
    public Polarity polarity;                   // current polarity

    


    private void OnEnable()
    {
        // disable the shot after X secs
        Invoke("disableMe", myLifeSpan);
    }
    private void Start()
    {
        // if null  finds the player and own rigid body
        if (rigidBody == null)
        {
            rigidBody = gameObject.GetComponent<Rigidbody2D>();
        }
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;

        }
    }
    void FixedUpdate()
    {
        // moves towards the player
        Vector2 direction = (Vector2)target.position - rigidBody.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rigidBody.angularVelocity = -angleChangingSpeed * rotateAmount;
        rigidBody.velocity = transform.up * movementSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check collision with boundaries only
        if (collision.gameObject.tag == "boundaries")
        {
            disableMe();
        }
    }

    void disableMe()
    {
        // return to pool
        Destroy(gameObject);
    }

    // used by the player to test collision
    public void hasCollided()
    {
        disableMe();
    }

}
