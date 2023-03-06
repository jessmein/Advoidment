using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour
{
    [SerializeField]
    private float basePercentChangeToSpawn = 20.0f;
    private float percentChanceToSpawn;

    public List<Advertisement> ads = new List<Advertisement>();
    public Stack<Advertisement> activeAds = new Stack<Advertisement>();
    public int maxActiveAds = 3;

    private Timer timer;
    private float prevTime;
    private System.Random rand = new System.Random();

    private bool activeAdComplete = false;
    private bool adsActive = false;
    private AdDifficulty activeAdDifficulty;

    private float gpValue = 2.0f;
    private float gracePeriod;

    public bool AdsActive { get { return adsActive; } }
    public bool ActiveAdComplete { get { return activeAdComplete; } set { activeAdComplete = value; } }
    public AdDifficulty ActiveAdDifficulty { get { return activeAdDifficulty; } set { activeAdDifficulty = value; } }
    public float PercentChanceToSpawn { get { return percentChanceToSpawn; } set { percentChanceToSpawn = value; } }
    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("TimerTest").GetComponent<Timer>();
        prevTime = timer.TimeLeft;

        percentChanceToSpawn = basePercentChangeToSpawn;

        activeAds.Clear();

        gracePeriod = gpValue;
}

// Update is called once per frame
void Update()
    {


        if (gracePeriod <= 0) {
            if (activeAds.Count < maxActiveAds) {
                ProcAd();
            }
        }


        if (activeAds.Count > 0) {
            if (activeAdComplete) {
                activeAdComplete = false;
                Debug.Log($"Ad difficulty is {activeAdDifficulty}... adding {(float)activeAdDifficulty} seconds");
                timer.AddTime((float)activeAdDifficulty);

                activeAds.Pop();

                gracePeriod = gpValue;

                if (activeAds.Count > 0) {
                    activeAds.Peek().UnpauseAd();
                }
            }

            adsActive = true;
        }
        else
        {
            adsActive = false;
        }

        gracePeriod -= Time.deltaTime;
    }

    void ProcAd() {
        if ((int) timer.TimeLeft != (int) prevTime) {
            int r = rand.Next(0, 100) + 1;
            //Debug.Log($"{r} || {percentChanceToSpawn}");
            if (r <= percentChanceToSpawn) {
                percentChanceToSpawn = basePercentChangeToSpawn;

                int s = rand.Next(0, ads.Count);
                Advertisement ad = ads[s];
                ad.CreateAd();

                if (activeAds.Count > 0) {
                    activeAds.Peek().PauseAd();
                }

                gracePeriod = gpValue;

                activeAds.Push(ad);
            }
        }

        prevTime = timer.TimeLeft;
    }
}
