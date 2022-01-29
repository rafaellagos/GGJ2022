using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Endless : MonoBehaviour
{
    
    [SerializeField] private float scrollingSpeed;
    [SerializeField] private float topPosition;
    [SerializeField] private float botPosition;

    private float randomPosition;
   
    void FixedUpdate()
    {
        transform.Translate(Vector2.down * scrollingSpeed * Time.deltaTime);

        if (transform.position.y <= botPosition)
        {
            if (this.tag.Equals("Clouds"))
            {
                randomPosition = Random.Range(0, 5);
            }
           
            transform.position = new Vector3(0f, topPosition + randomPosition, 0f);
        }
    }
}
