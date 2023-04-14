using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BasketBallAd : Advertisement
{
    private AdManager adManager;
    private GameObject basketball;
    private Vector3 scale;
    private bool newBool = false;
    public override bool Paused { get { return paused; } }
    public override bool Completed { get { return completed; } set { completed = value; } }

    //Proper Scale is (1,1)

    public override void CreateAd()
    {
        Instantiate(gameObject);
        gameObject.SetActive(true);
    }

    public override void ForceCloseAd() {
        StartCoroutine(waiter());
    }

    protected override IEnumerator waiter()
    {
        yield return new WaitForSeconds(1);
        newBool = true;

        yield return new WaitForSeconds(0.15f);
        yield return Completed = true;
        Debug.Log("BYE");
        //gameManager.activeAds = 0;
        Destroy(gameObject);
    }

    protected override IEnumerator waiterDeath()
    {
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        adManager = GameObject.Find("AdManager").GetComponent<AdManager>();

        Difficulty = AdDifficulty.Hard;
        basketball = GameObject.Find("Basketball");

        scaleChange = new Vector3(0.01f, 0.01f, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        scale = transform.localScale;
        if (transform.localScale.x <= 1 && newBool == false)
        {
            ChangeScale(true);
        }

        //Scale down at end
        if (newBool == true && transform.localScale.x >= 0.1)
        {
            ChangeScale(false);
        }

        if (basketball.GetComponent<ShootBall>().hit)
        {
            StartCoroutine(waiter());
        }
        if (adManager == null)
        {

        }
        else
        {
            adManager.ActiveAdComplete = Completed;
            adManager.ActiveAdDifficulty = Difficulty;
        }   
        
    }
}
