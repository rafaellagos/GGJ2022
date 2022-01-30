using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotController : MonoBehaviour
{
    //Imports
    [Tooltip("0 = blue, 1 = red")]
    public GameObject[] shots;
    public bool red;
    public enum direction // your custom enumeration
    {
        bottom,
        bottomRight,
        bottomleft,
        right,
        left,
        top,
        topRight,
        topLeft,
        onPlayer
    };
    public direction currectDirection = direction.bottom;

    public float speed;
    public Vector2 moveDirection;

    bool move;


    // Start is called before the first frame update
    void Start()
    {
        move = false;
        
        spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if(move)
        {
            transform.Translate(moveDirection.x * speed * Time.deltaTime, moveDirection.y * speed * Time.deltaTime, 0);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void spawn()
    {
        // set direction
        switch(currectDirection)
        {
            case direction.bottom:
                moveDirection = new Vector2(0, -1);
                break;
            case direction.bottomleft:
                moveDirection = new Vector2(-1, -1);
                break;
            case direction.bottomRight:
                moveDirection = new Vector2(1, -1);
                break;
            case direction.right:
                moveDirection = new Vector2(1, 0);
                break;
            case direction.left:
                moveDirection = new Vector2(-1, 0);
                break;
            case direction.top:
                moveDirection = new Vector2(0, 1);
                break;
            case direction.topLeft:
                moveDirection = new Vector2(-1,1);
                break;
            case direction.topRight:
                moveDirection = new Vector2(1, 1);
                break;
            case direction.onPlayer:
                Vector2 temp = new Vector2(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y);
                moveDirection = new Vector2(temp.x - transform.position.x , temp.y - transform.position.y );
                moveDirection.Normalize();
                break;
        }
        //enable the correct color
        if (red)
        {
            shots[1].SetActive(true);
            shots[0].SetActive(false);
        }
        else
        {
            shots[1].SetActive(false);
            shots[0].SetActive(true);
        }
        move = true;
    }
}
