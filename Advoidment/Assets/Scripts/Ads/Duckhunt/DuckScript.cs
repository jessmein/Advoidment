using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckScript : MonoBehaviour
{
    // Start is called before the first frame update

    //spawn ducks between X 7.7 to -7.7
    //Y: 4.5 to -4.5
    private bool goingRight;
    System.Random random = new System.Random();
    private float totalTime = 0;

    void Start()
    {
        if(random.Next(0, 2) == 0)
        {
            goingRight = true;
        }
        else
        {
            goingRight = false;
        }
    }

    void DuckMove()
    {
        if (transform.position.x <=  -4.5 )
        {
            goingRight = true;
        }
        else if (transform.position.x >= 4.5)
        {
            goingRight = false;
        }

        if (totalTime >= 2.0f)
        {
            totalTime = 0;
            if (transform.position.y <= -4)
            {
                Vector2 force = new Vector2(0, 0.5f);
                gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            }
            else
            {
                if (goingRight)
                {
                    Vector2 force = new Vector2(3, 3);
                    gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                }
                else
                {
                    Vector2 force = new Vector2(-3, 3);
                    gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(totalTime);
        totalTime += Time.deltaTime;
        DuckMove();
    }
    private void OnMouseDown()
    {
            Debug.Log("Clicked");
            Destroy(gameObject);
    }
}
