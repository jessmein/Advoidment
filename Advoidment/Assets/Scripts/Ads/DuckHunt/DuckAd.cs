using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckAd : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] GameObject duck;
    [SerializeField] AudioSource gunshot;
    System.Random randomNum = new System.Random();
    private GameObject crossHair;


    void Start()
    {
        crossHair = GameObject.Find("Crosshair");
        //Generate 10 random Ducks
        Quaternion rotation = new Quaternion(0, 0, 0, 0);
        for (int x = 0; x < 5; x++)
        {
            Vector3 position = new Vector3(randomNum.Next(-80, 81) / 10, randomNum.Next(-45, 46) / 10,0);
            Instantiate(duck, position, rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        crossHair.transform.position = mousePos;
    }
}
