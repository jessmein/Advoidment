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

    public bool ActiveAdComplete { get { return activeAdComplete; } set { activeAdComplete = value; } }
    public float PercentChanceToSpawn { get { return percentChanceToSpawn; } set { percentChanceToSpawn = value; } }
    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("TimerTest").GetComponent<Timer>();
        prevTime = timer.timeLeft;

        percentChanceToSpawn = basePercentChangeToSpawn;

        activeAds.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeAds.Count < maxActiveAds) {
            ProcAd();
        }

        if (activeAds.Count > 0) {
            if (activeAdComplete) {
                activeAdComplete = false;
                activeAds.Pop();

                if (activeAds.Count > 0) {
                    activeAds.Peek().UnpauseAd();
                }
            }
        }
    }

    void ProcAd() {
        if ((int) timer.timeLeft != (int) prevTime) {
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

                activeAds.Push(ad);
            }
        }

        prevTime = timer.timeLeft;
    }
}
