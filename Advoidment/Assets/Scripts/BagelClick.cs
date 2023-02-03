using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BagelClick : MonoBehaviour {
    // Start is called before the first frame update
    //[SerializeField] GameObject begal;
    public int Score { get { return score; } }

    public GameManager gameManager;

    public static int score = 0;

    public GameObject bagel;
    public float bagelRadius;

    private Text scoreDisplay;
    private Timer timeManager;

    void Start()
    {
        bagelRadius = bagel.transform.localScale.x / 2.0f;

        scoreDisplay = GameObject.Find("Score Display").GetComponent<Text>();

        timeManager = FindObjectOfType<Timer>();
    }

    public void OnClick(InputValue value)
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 test = new Vector2(bagel.transform.position.x, bagel.transform.position.y);

        float distance = Vector2.Distance(mousePos, test);

        if (distance <= bagelRadius && gameManager.activeAds == 0) {
            score++;
            timeManager.AddTime();
        }

    }

    // Update is called once per frame
    void Update()
    {
        scoreDisplay.text = "Score: " + score;
    }

}
