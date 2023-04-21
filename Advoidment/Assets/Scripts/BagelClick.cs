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


    public static int score = 0;

    public GameObject bagel;
    public float bagelRadius;

    public Bars scoreBar;
    public GameObject point; // gets the point prefab

    public Image skipAdImage;

    private AdManager adManager;
    private GameManager gameManager;
    private Text scoreDisplay;
    private Timer timeManager;

    private Animator bagelClick;

    void Start()
    {
        adManager = GameObject.Find("AdManager").GetComponent<AdManager>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        bagelRadius = bagel.transform.localScale.x / 2.0f;

        scoreDisplay = GameObject.Find("Score Display").GetComponent<Text>();

        timeManager = FindObjectOfType<Timer>();

        bagelClick = bagel.GetComponent<Animator>();

        scoreBar.SetMax(100);
    }

    public void OnClick(InputValue value)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 bagelPos = new Vector2(bagel.transform.position.x, bagel.transform.position.y);
        Vector2 imagePos = new Vector2(skipAdImage.transform.position.x, skipAdImage.transform.position.y);

        float distance = Vector2.Distance(mousePos, bagelPos);

        if (distance <= bagelRadius && adManager.activeAds.Count == 0) {
            bagelClick.SetTrigger("btnClicked"); // starts the animator
            score++;
            adManager.PercentChanceToSpawn += 0.5f;
            //timeManager.AddTime(1); // increases the time
            scoreBar.IncreaseScoreMeter(score);

            //Instantiate(point);
        }

        if (mousePos.x >= imagePos.x - (skipAdImage.rectTransform.rect.width / 2f) &&
            mousePos.x <= imagePos.x + (skipAdImage.rectTransform.rect.width / 2f) &&
            mousePos.y >= imagePos.y - (skipAdImage.rectTransform.rect.height / 2f) &&
            mousePos.y <= imagePos.y + (skipAdImage.rectTransform.rect.height / 2f)) {
            gameManager.skipAd.Activated = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreDisplay.text = "Score: " + score;
    }
}
