using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Programmer: Jessica Niem
/// Date: 2/1/23
/// Description: Timer is used to keep track of how much time player has to solve all the ads and can be increased by 1 everytime the player clicks the bagel
/// </summary>
public class Timer : MonoBehaviour
{
    private float timeLeft;
    private AdManager adManager;

    // used to update UI
    private Text timeDisplay;
    public Bars timeBar;
    public Text freezeTimeText;
    public Image freezeTimeImage;

    private float currentFreezeTime;
    private float freezeTime = 8.0f;
    private int numFreezeTime;
    private bool freezeTimeActive;

    public float TimeLeft { get { return timeLeft; } }
    public int NumFreezeTime { get { return numFreezeTime; } set { numFreezeTime = value; } }
    public bool FreezeTimeActive { get { return freezeTimeActive; } set { freezeTimeActive = value; } }

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 91; // start with 90 seconds // set to 10 for testing
        timeDisplay = GameObject.Find("Timer Display").GetComponent<Text>();
        adManager = FindObjectOfType<AdManager>();

        timeBar.SetMax(timeLeft);
    }

    // Update is called once per frame
    void Update()
    {
        freezeTimeText.text = "" + numFreezeTime;

        if (numFreezeTime <= 0 || currentFreezeTime != freezeTime) {
            freezeTimeImage.color = Color.gray;
        } 
        
        if (numFreezeTime > 0 && currentFreezeTime == freezeTime) {
            freezeTimeImage.color = Color.white;
        }

        if (freezeTimeActive) {
            //bar and other time ui thing needs to be gray
            currentFreezeTime -= Time.deltaTime;
            timeDisplay.color = Color.gray;
            timeBar.image.color = Color.gray;

            if (currentFreezeTime <= 0f) {
                //make ui not gray
                freezeTimeActive = false;
                currentFreezeTime = freezeTime;
                timeDisplay.color = Color.white;
                timeBar.image.color = Color.blue;
            }
            return;
        }

        currentFreezeTime = freezeTime;

        // deduct the time
        //if (adManager.AdsActive)
        //{
        //}
        if (timeLeft > 0) {
            timeLeft -= Time.deltaTime;
            timeBar.SetTimeMeter(timeLeft);
        } else {
            // end game
            SceneManager.LoadScene("End Screen");
        }

        // if there's 15 seconds left, change text color to red
        timeDisplay.color = timeLeft < 16 ? Color.red : Color.black;

        timeDisplay.text = ((int)timeLeft / 60) + ":"; // update the time, get the minute

        int seconds = ((int)timeLeft % 60); // calculate the seconds

        // if the seconds are single digits, add a zero before it, else just display the seconds
        timeDisplay.text += seconds < 10 ? "0" + seconds : seconds;
    }

    // used whenever the bagel is clicked
    public void AddTime(float time)
    {
        timeLeft += time;
        timeBar.SetTimeMeter(timeLeft);
    }
}
