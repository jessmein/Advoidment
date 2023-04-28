using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BagelClick : MonoBehaviour {
    // Start is called before the first frame update
    //[SerializeField] GameObject begal;
    public int Score { get { return score; } set { score = value; } }

    public static int score = 0;

    public GameObject bagel;
    public float bagelRadius;

    public Bars scoreBar;
    public GameObject point; // gets the point prefab

    public Image skipAdImage;
    public Image doubleClickImage;
    public Image freezeTimeImage;

    public Point particlePool;

    public GameObject pauseMenu;

    private AdManager adManager;
    private GameManager gameManager;
    private Text scoreDisplay;
    private Timer timeManager;

    public bool doubleClick;
    private int numDoubleClicks;
    private float doubleClickGracePeriod;
    private float doubleClickGPValue = 3.0f;

    private Animator bagelClick;
    private GameObject canvas;

    public int NumDoubleClicks { get { return numDoubleClicks; } set { numDoubleClicks = value; } }

    void Start()
    {
        adManager = GameObject.Find("AdManager").GetComponent<AdManager>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        bagelRadius = bagel.transform.localScale.x / 2.0f;

        scoreDisplay = GameObject.Find("Score Display").GetComponent<Text>();

        timeManager = FindObjectOfType<Timer>();

        bagelClick = bagel.GetComponent<Animator>();

        scoreBar.SetMax(gameManager.powerUpThreshold);

        canvas = GameObject.Find("Canvas");
    }

    public void OnClick(InputValue value)
    {
        if (!pauseMenu.activeInHierarchy)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 bagelPos = new Vector2(bagel.transform.position.x, bagel.transform.position.y);
            Vector2 skipImagePos = new Vector2(skipAdImage.transform.position.x, skipAdImage.transform.position.y);
            Vector2 doubleClickPos = new Vector2(doubleClickImage.transform.position.x, doubleClickImage.transform.position.y);
            Vector2 freezeTimePos = new Vector2(freezeTimeImage.transform.position.x, freezeTimeImage.transform.position.y);

            float distance = Vector2.Distance(mousePos, bagelPos);

            if (distance <= bagelRadius && adManager.activeAds.Count == 0)
            {
                bagelClick.SetTrigger("btnClicked"); // starts the animator
                score += doubleClick ? 2 : 1;
                adManager.PercentChanceToSpawn += 0.5f;
                scoreBar.IncreaseScoreMeter(score);

                ParticleSystem pSys = particlePool.GetParticle();

                if (doubleClick)
                {
                    particlePool.SetMaterial(true);
                }
                else
                {
                    particlePool.SetMaterial(false);
                }

                // sets the particle system to where the mouse is and plays
                pSys.transform.position = new Vector3(mousePos.x, mousePos.y, -4);
                //Debug.Log(pSys.time);
                //pSys.time = 1.0f;
                //Debug.Log(pSys.time);
                pSys.Emit(1);
            }

            if (mousePos.x >= skipImagePos.x - (skipAdImage.rectTransform.rect.width / 2f) &&
                mousePos.x <= skipImagePos.x + (skipAdImage.rectTransform.rect.width / 2f) &&
                mousePos.y >= skipImagePos.y - (skipAdImage.rectTransform.rect.height / 2f) &&
                mousePos.y <= skipImagePos.y + (skipAdImage.rectTransform.rect.height / 2f))
            {
                gameManager.skipAd.Activated = true;
            }

            if (mousePos.x >= doubleClickPos.x - (skipAdImage.rectTransform.rect.width / 2f) &&
                mousePos.x <= doubleClickPos.x + (skipAdImage.rectTransform.rect.width / 2f) &&
                mousePos.y >= doubleClickPos.y - (skipAdImage.rectTransform.rect.height / 2f) &&
                mousePos.y <= doubleClickPos.y + (skipAdImage.rectTransform.rect.height / 2f)) {

                //Only activate if double click is not already activated
                //and if there are a sufficient number of the power up
                if (!doubleClick && numDoubleClicks > 0) {
                    doubleClick = true;
                    numDoubleClicks--;
                }
            }

            if (mousePos.x >= freezeTimePos.x - (skipAdImage.rectTransform.rect.width / 2f) &&
                mousePos.x <= freezeTimePos.x + (skipAdImage.rectTransform.rect.width / 2f) &&
                mousePos.y >= freezeTimePos.y - (skipAdImage.rectTransform.rect.height / 2f) &&
                mousePos.y <= freezeTimePos.y + (skipAdImage.rectTransform.rect.height / 2f)) {

                if (!timeManager.FreezeTimeActive && timeManager.NumFreezeTime > 0) {
                    timeManager.FreezeTimeActive = true;
                    timeManager.NumFreezeTime--;
                }
            }
        }
    }

    public void OnSkip(InputValue value) {
        gameManager.skipAd.Activated = true;
    }

    public void OnDoubleClick(InputValue value) {
        if (!doubleClick && numDoubleClicks > 0) {
            doubleClick = true;
            numDoubleClicks--;
        }
    }

    public void OnFreezeTime(InputValue value) {
        if (!timeManager.FreezeTimeActive && timeManager.NumFreezeTime > 0) {
            timeManager.FreezeTimeActive = true;
            timeManager.NumFreezeTime--;
        }
    }

    public void OnPause(InputValue value) {
        pauseMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (doubleClickGracePeriod <= 0f) {
            doubleClickGracePeriod = doubleClickGPValue;
            doubleClick = false;
        }

        if (doubleClickGracePeriod != doubleClickGPValue || numDoubleClicks <= 0) {
            doubleClickImage.color = Color.gray;
        }

        if (doubleClickGracePeriod == doubleClickGPValue && numDoubleClicks > 0) {
            doubleClickImage.color = Color.white;
        }

        scoreDisplay.text = "Score: " + score;

        if (doubleClick) {
            doubleClickGracePeriod -= Time.deltaTime;
        }

        doubleClickImage.GetComponentInChildren<Text>().text = "" + numDoubleClicks;
    }
}
