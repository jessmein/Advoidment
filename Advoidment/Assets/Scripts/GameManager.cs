using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUp {
    SkipAd,
    DoubleClick
}

public class GameManager : MonoBehaviour
{
    public BagelClick bagelClick;
    public SkipAd skipAd;

    public int powerUpThreshold = 50;

    private bool incrementSkips = false;
    private bool incrementDoubleClick = false;
    private int lastIncrementedScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bagelClick.Score > 0 && 
            bagelClick.Score - lastIncrementedScore >= powerUpThreshold) {

            PowerUp powerUp = (PowerUp)Random.Range(0, 2);

            Debug.Log($"Picked {powerUp.ToString()}");

            switch (powerUp) {
                case PowerUp.SkipAd:
                    incrementSkips = true;
                    break;

                case PowerUp.DoubleClick:
                    incrementDoubleClick = true;
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
    }
}
