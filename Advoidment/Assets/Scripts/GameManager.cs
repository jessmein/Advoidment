using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BagelClick bagelClick;
    public SkipAd skipAd;

    private bool incrementSkips = false;
    private int lastIncrementedScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bagelClick.Score > 0 && 
            bagelClick.Score % 100 == 0 && 
            bagelClick.Score != lastIncrementedScore) {
            incrementSkips = true;
            lastIncrementedScore = bagelClick.Score;
        }

        if (incrementSkips) {
            skipAd.NumSkips++;
            incrementSkips = false;
        }
    }
}
