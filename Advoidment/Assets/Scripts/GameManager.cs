using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Timer timer;
    public SquareAd puzzle;
    public int activeAds = 0;

    private float prevTime;
    private System.Random rand = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        prevTime = timer.timeLeft;
    }

    // Update is called once per frame
    void Update()
    {
        activeAds = Mathf.Clamp(activeAds, 0, 1);

        if (timer.timeLeft != prevTime) {
            int r = rand.Next(0, 10000) + 1;
            if (r <= 6 && activeAds < 1) {
                puzzle.CreateAd();
                activeAds++;
            }
        }

        prevTime = timer.timeLeft;
    }


}
