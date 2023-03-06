using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairMove : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        crossHairMove();
    }

    void crossHairMove()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        this.gameObject.GetComponent<Rigidbody2D>().position = mousePos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
    }

}
