using System;
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
    private GameObject game;
    private List<GameObject> ducks = new List<GameObject>();
    private int totalNumOfDucks = 5;

    public AdManager adManager;

    public int numberOfDeadDucks = 0;

    public override bool Paused { get { return paused; } }
    public override bool Completed { get { return completed; } set { completed = value; } }

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
        yield return new WaitForSeconds(0.1f);
        yield return Completed = true;

        gameObject.SetActive(false);

        Destroy(this);
        Destroy(game);
        Debug.Log("BYE");
    }

    protected override IEnumerator waiterDeath()
    {
        return null;
    }

    void Start()
    {
        adManager = GameObject.Find("AdManager").GetComponent<AdManager>();
        Difficulty = AdDifficulty.Easy;

        crossHair = GameObject.Find("Crosshair");
        game = GameObject.Find("DuckHuntGame(Clone)");
        //Generate 10 random Ducks
        Quaternion rotation = new Quaternion(0, 0, 0, 0);
        for (int x = 0; x < totalNumOfDucks; x++)
        {
            Vector3 position = new Vector3(
                randomNum.Next(
                    (int) (this.transform.position.x - 100),
                    (int)(this.transform.position.x + 100)), 
                randomNum.Next(
                    (int)(this.transform.position.y - 100),
                    (int)(this.transform.position.y + 100)),
                0);

            ducks.Add(Instantiate(duck, position, rotation, game.transform));
        }
    }

    // Update is called once per frame
    void Update()
    {
        adManager.ActiveAdComplete = Completed;
        adManager.ActiveAdDifficulty = Difficulty;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        crossHair.transform.position = mousePos;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            gunshot.Play();
        }

        if (numberOfDeadDucks == totalNumOfDucks)
        {
            StartCoroutine(waiter());
        }

    }
}
