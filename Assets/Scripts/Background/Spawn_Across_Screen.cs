using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Across_Screen : MonoBehaviour
{
    private enum SpawnPosition { horizontal, vertical};
    [SerializeField] SpawnPosition spawn;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (spawn == SpawnPosition.vertical)
        {
            if (collision.transform.position.y > 0)
            {
                Vector2 temp = new Vector2(collision.transform.position.x, collision.transform.position.y * -1 + 0.5f);
                collision.transform.position = new Vector2(temp.x, temp.y);
            }
            else
            {
                Vector2 temp = new Vector2(collision.transform.position.x, collision.transform.position.y * -1 - 0.5f);
                collision.transform.position = new Vector2(temp.x, temp.y);
            }


        }

        if (spawn == SpawnPosition.horizontal)
        {
            if (collision.transform.position.x > 0)
            {
                Vector2 temp = new Vector2(collision.transform.position.x * -1 + 0.5f, collision.transform.position.y);
                collision.transform.position = new Vector2(temp.x , temp.y);
            }
            else
            {
                Vector2 temp = new Vector2(collision.transform.position.x * -1 - 0.5f, collision.transform.position.y);
                collision.transform.position = new Vector2(temp.x , temp.y);
            }

        }
    } 
}
