using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgingAd : MonoBehaviour
{
    public GameObject player;

    //Player variables
    private bool isGrounded;
    private float jumpForce;
    private float gravity;
    private float verticalMove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Debug.Log("JUMP");
        }
    }
}
