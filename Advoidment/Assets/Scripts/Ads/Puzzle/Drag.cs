using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public bool dragging = false;
    private Vector3 offset;
    public Vector3 originalPosition;

    public GameObject Ad;
    public Vector3 scale;

    public GameObject target;

    public bool dragged;


    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        originalPosition = transform.position;
        scale = Ad.transform.localScale;

        transform.position = new Vector2(
            originalPosition.x + (Random.Range(-90.0f, 90.0f)), originalPosition.y + (Random.Range(-90.0f, 90.0f))
            );

        dragged = false;
    }

    // Update is called once per frame
    void Update()
    {
        scale = Ad.transform.localScale;
        if (dragging)
        {
            dragged = true;
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;

            
            //Right bounds
            if (transform.localPosition.x > .9)
            {
                transform.position = new Vector3(originalPosition.x + (0.89f * scale.x), transform.position.y, transform.position.z);
            }

            //Left bounds
            if (transform.localPosition.x < -1.66)
            {
                transform.position = new Vector3(originalPosition.x - (1.67f * scale.x), transform.position.y, transform.position.z);
            }

            //Upper bounds
            if (transform.localPosition.y > 1.25)
            {
                transform.position = new Vector3(transform.position.x, originalPosition.y + (1.24f * scale.y), transform.position.z);
            }

            //Lower bounds
            if (transform.localPosition.y < -0.75)
            {
                transform.position = new Vector3(transform.position.x, originalPosition.y - (0.74f * scale.y), transform.position.z);
            }
        }
    }

    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
    }
    
}
