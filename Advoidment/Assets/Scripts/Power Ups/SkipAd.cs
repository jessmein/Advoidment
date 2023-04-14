using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipAd : MonoBehaviour {
    private AdManager adManager;
    private bool activated = false;
    private int numSkips = 0;
    private float skipAdGracePeriod;

    public bool Activated { get { return activated; } set { activated = value; } }
    public int NumSkips { get { return numSkips; } set { numSkips = value; } }

    // Start is called before the first frame update
    void Start() {
        adManager = GameObject.Find("AdManager").GetComponent<AdManager>();

        skipAdGracePeriod = adManager.GPValue;
    }

    // Update is called once per frame
    void Update() {
        if (numSkips > 0) {
            if (skipAdGracePeriod <= 0) {
                if (activated) {
                    if (adManager.AdsActive) {
                        Advertisement ad = GameObject.FindObjectOfType<Advertisement>();
                        ad.ForceCloseAd();
                        skipAdGracePeriod = adManager.GPValue;
                        numSkips--;
                    }

                    activated = false;
                }
            }

        }

        skipAdGracePeriod -= Time.deltaTime;
    }
}
