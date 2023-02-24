using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class blockBuster : MonoBehaviour
{
    public GameObject goal;
    public GameObject block;
    public GameObject ball;

    public GameObject startingBall;

    private int numBalls;
    private float angle;
    private bool playerTurn;

    public float Angle { get { return angle; } }

    // Start is called before the first frame update
    void Start()
    {
        numBalls = 10;
        playerTurn = true;

        SpawnRow();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerTurn)
        {
            ApproachPlayer();
            SpawnRow();
        }
    }

    // spawns the balls and shoots the ball towards the mouse position
    void onClick()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        CalculateAngle(mousePos);
    }

    // calculates the angle
    private void CalculateAngle(Vector3 mouse)
    {
        Debug.Log("I'm calculating " + mouse);
    }

    // moves the blocks on the screen closer to the circle on the bottom
    private void ApproachPlayer()
    {

    }

    // delete the instance of the block
    public void DestroyBlock()
    {

    }

    private void SpawnRow()
    {

    }
}
