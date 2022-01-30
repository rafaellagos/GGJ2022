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
    public moveType currentMove = moveType.upDown;


    public enum Polarity { white, black }       // polarity options
    [Header("Polarity of the enemy")]   
    //definesPolarity
    public Polarity polarity;                   // current polarity


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
