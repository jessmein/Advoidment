using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUp {
    SkipAd = 0,
    DoubleClick = 1,
    FreezeTime = 2
}

public class GameManager : MonoBehaviour
{
    public BagelClick bagelClick;
    public SkipAd skipAd;

    public int powerUpThreshold = 50;

    private Timer timeManager;

    private bool incrementSkips = false;
    private bool incrementDoubleClick = false;
    private bool incrementFreezeTime = false;
    private int lastIncrementedScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        timeManager = FindObjectOfType<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bagelClick.Score > 0 && 
            bagelClick.Score - lastIncrementedScore >= powerUpThreshold) {

            PowerUp powerUp = (PowerUp)(Random.Range(0, 300) % 3);

            Debug.Log($"Picked {powerUp.ToString()}");

            switch (powerUp) {
                case PowerUp.SkipAd:
                    incrementSkips = true;
                    break;

                case PowerUp.DoubleClick:
                    incrementDoubleClick = true;
                    break;

                case PowerUp.FreezeTime:
                    incrementFreezeTime = true;
                    break;

                default:
                    incrementSkips = true;
                    break;
            }

            lastIncrementedScore = bagelClick.Score;
        }

        if (incrementSkips) {
            skipAd.NumSkips++;
            incrementSkips = false;
        }

        if (incrementDoubleClick) {
            bagelClick.NumDoubleClicks++;
            incrementDoubleClick = false;
        }

        if (incrementFreezeTime) {
            timeManager.NumFreezeTime++;
            incrementFreezeTime = false;
        }
    }

    public void RestartGame()
    {
        FindObjectOfType<Timer>().Start();
        bagelClick.OnPause(null);
        bagelClick.Score = 0;
        FindObjectOfType<AdManager>().activeAds.Clear();
    }
}
