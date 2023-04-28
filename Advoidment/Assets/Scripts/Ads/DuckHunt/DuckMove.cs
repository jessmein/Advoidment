using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMove : MonoBehaviour
{
    // Start is called before the first frame update
    private float velocity = 0.5f;
    private System.Random randomNumGen = new System.Random();
    private Vector3 direction;
    private float secTillDirChange = 3.0f;
    private float currentX;
    private float currentY;
    private Rigidbody2D duckRB;
    private bool isDead = false;
    private DuckAd duckAd;
    [SerializeField] Sprite deadDuck;
    
    void Start()
    {
        currentX = randomNumGen.Next(-10,11);
        currentY = randomNumGen.Next(-10, 11);
        direction = new Vector3(currentX, currentY, 0);
        duckRB = GetComponent<Rigidbody2D>();
        duckAd = FindObjectOfType<DuckAd>();

    }

    private void ChangeDirection()
    {
        currentX = randomNumGen.Next(-10, 11);
        currentY = randomNumGen.Next(-10, 11);
        direction = new Vector3(currentX, currentY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            duckRB.velocity = new Vector2(0,0);
        }
        else
        {
            if (secTillDirChange <= 0)
            {
                ChangeDirection();
                secTillDirChange = 3.0f;
            }
            else
            {
                if (transform.position.x < -8)
                {
                    direction = new Vector3(-currentX, currentY, 0);
                }
                if (transform.position.x > 8)
                {
                    direction = new Vector3(-currentX, currentY, 0);
                }
                if (transform.position.y < -4)
                {
                    direction = new Vector3(currentX, -currentY, 0);
                }
                if (transform.position.y > 4)
                {
                    direction = new Vector3(currentX, -currentY, 0);
                }
            }
            secTillDirChange -= Time.deltaTime;
            duckRB.velocity = direction / 2;
        }

    }

    private void OnMouseDown()
    {
        if (!isDead) {
            SpriteRenderer duckSprite = GetComponent<SpriteRenderer>();
            duckSprite.sprite = deadDuck;
            duckSprite.color = Color.red;
            isDead = true;
            duckAd.numberOfDeadDucks++;
        }
    }


}

