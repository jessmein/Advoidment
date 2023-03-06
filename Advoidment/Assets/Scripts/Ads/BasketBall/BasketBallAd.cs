using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBallAd : Advertisement
{
    private AdManager adManager;
    private GameObject basketball;
    public override bool Paused { get { return paused; } }
    public override bool Completed { get { return completed; } set { completed = value; } }

    public override void CreateAd()
    {
        Instantiate(gameObject);
        gameObject.SetActive(true);
    }

    protected override IEnumerator waiter()
    {
        yield return new WaitForSeconds(1);
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
        basketball = GameObject.Find("Basketball");
    }

    // Update is called once per frame
    void Update()
    {
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
        }   
        
    }
}
