using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveControl : MonoBehaviour
{
    //defines kind of move behavior
    public enum moveType // your custom enumeration
    {
        upDown,
        upDownWithBriefStop,
        upMidScreenStay,
        upMidScreenStayLeave,
        upMidScreenLeaveLeft,
        upMidScreenLeaveRight,
        upMidScreenPatrol
    };
    [Header("Move type")]
    public moveType currentMove;// = moveType.upDown;


    public enum Polarity { white, black }       // polarity options
    [Header("Polarity of the enemy")]   
    //definesPolarity
    public Polarity polarity;                   // current polarity

    public Enemy_Controller enemy_Controller;


    private void Awake()
    {
        currentMove = (moveType)Random.Range(0, 6);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (enemy_Controller == null) enemy_Controller = gameObject.GetComponent<Enemy_Controller>();

        if(enemy_Controller.polarity == Enemy_Controller.Polarity.black)
        {
            polarity = Polarity.black;
        }
        else
        {
            polarity = Polarity.white;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
