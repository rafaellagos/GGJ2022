using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotPatternController : MonoBehaviour
{
    //defines the shot type
    public enum shotPattern // your custom enumeration
    {
        bottom,             // shots to the bottom only
        atPlayer,           // shots at player
        nova3Way,           // bottom, bottom right, bottom left
        nova5Way,           // 3 way + left and right
        nova8Way,           // all directions
        homing              // chases player       
    };
    public shotPattern currentPattern = shotPattern.bottom;

    public GameObject shotObject;
    public EnemyMoveControl enemyMoveControl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
