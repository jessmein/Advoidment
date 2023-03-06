using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckHuntAd : MonoBehaviour
{
    private GameObject crossHair;
    private Vector2 mousePos;
    private Camera mainCamera;
    private GameObject duck1;
    private System.Random rand = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        duck1 = GameObject.Find("Duck");
        GenerateDucks();
    }

    private void crossHairMove()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        //crossHair.GetComponent<Rigidbody2D>().position = mousePos;
    }
    private void GenerateDucks()
    {
        for(int x =0; x<10;x++)
        {
            Vector3 duckPosition = new Vector3(rand.Next(-7,7) + .7f, rand.Next(-4, 4) + .5f,0);
            Quaternion duckRot = new Quaternion();
            Instantiate(duck1, duckPosition, duckRot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        crossHairMove();
    }
}
