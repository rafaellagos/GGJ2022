using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Enemy_Controller : MonoBehaviour
{
    public Polarity polarity;
    public enum Polarity { white, black }


    void Start()
    {
        GetRandomPolarity();
    }

    public void GetRandomPolarity() 
    {
        int temp = Random.Range(0,2);
        if (temp == 0)
        {
            polarity = Polarity.white;
            transform.gameObject.tag = "White";
        }
        if (temp == 1)
        {
            polarity = Polarity.black;
            transform.gameObject.tag = "Black";
        }
    }
}
