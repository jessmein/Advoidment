using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckAd : Advertisement
{
    // Start is called before the first frame update
    
    [SerializeField] GameObject duck;
    [SerializeField] AudioSource gunshot;
    System.Random randomNum = new System.Random();
    private GameObject crossHair;
    private int totalNumOfDucks = 5;

    public AdManager adManager;

    protected int numberOfDeadDucks = 0;

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

    void Start()
    {
        gunshot.Play();
        crossHair = GameObject.Find("Crosshair");
        //Generate 10 random Ducks
        Quaternion rotation = new Quaternion(0, 0, 0, 0);
        for (int x = 0; x < totalNumOfDucks; x++)
        {
            Vector3 position = new Vector3(randomNum.Next(-80, 81) / 10, randomNum.Next(-45, 46) / 10,0);
            Instantiate(duck, position, rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        crossHair.transform.position = mousePos;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            gunshot.Play();
        }

        if(numberOfDeadDucks == totalNumOfDucks)
        {
            completed = true;
        }

    }
}
