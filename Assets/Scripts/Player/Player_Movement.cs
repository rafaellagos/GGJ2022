using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Player_Movement : MonoBehaviour
{
    public float moveSpeed;
    public float topSpeed;
    public float rotationSpeed;

    [SerializeField] private Transform player;
    [SerializeField] private Main_Controller main_Controller;
    [SerializeField] private Rigidbody2D rigidBody2D;

    private Vector2 movementAxisReceptor;

    private const string horizontalAxis = "Horizontal";
    private const string verticalAxis = "Vertical";

    public float speed = 5;
    public float gravity = -5;

    public PhotonView view;

    private void Start()
    {
        player = this.transform;
        rigidBody2D = this.GetComponent<Rigidbody2D>();

        main_Controller = GameObject.FindGameObjectWithTag("Main_Controller").GetComponent<Main_Controller>();
        view = GetComponent<PhotonView>();

     
    }

    void FixedUpdate()
    {
        if (view.IsMine)
        {
            Vector3 Movement = new Vector2(Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis));
            player.transform.position += Movement * speed * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Q))
            {
                main_Controller.ScreenShake();
            }
        }

    }

   /* [RPC]
    public void DebugRPC()
    {
        if (!photonView.isMine)
        {
            photonView.GetComponent<ItemContainer>().isLootable = true;
        }
    }*/





    public void ResetPlayerOrientation()
    {        
        player.position = new Vector2(0, -4);
        player.rotation = new Quaternion(0, 0, 0, 0);
        rigidBody2D.velocity = new Vector2(0, 0);
    }
}
