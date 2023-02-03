using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class BagelClick : MonoBehaviour {
    // Start is called before the first frame update
    //[SerializeField] GameObject begal;
    public int Score { get { return score; } }

    public static int score = 0;

    public GameObject bagel;
    public float bagelRadius;

    void Start()
    {
        bagelRadius = bagel.transform.localScale.x / 2.0f;
    }

    public void OnClick(InputValue value)
    {
        Vector2 mousePos = Input.mousePosition;

        if (Vector2.Distance(mousePos, bagel.transform.position) <= bagelRadius) {
            score++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(score);
    }

}
