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
    public float timeLeft;
    private Text timeDisplay; // used to update UI

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 91; // start with 90 seconds // set to 10 for testing
        timeDisplay = GameObject.Find("Timer Display").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // deduct the time
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            // end game
            SceneManager.LoadScene("End Screen");
        }

        // if there's 15 seconds left, change text color to red
        timeDisplay.color = timeLeft < 16 ? Color.red : Color.white;

        timeDisplay.text = ((int)timeLeft / 60) + ":"; // update the time, get the minute

        int seconds = ((int)timeLeft % 60); // calculate the seconds

        // if the seconds are single digits, add a zero before it, else just display the seconds
        timeDisplay.text += seconds < 10 ? "0" + seconds : seconds;
    }

    // used whenever the bagel is clicked
    public void AddTime()
    {
        timeLeft++;
    }
}
