using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaitingAd : Advertisement
{
    public CloseButton closeButton;
    public AdManager adManager;

    private bool newBool = false;

    public override bool Paused { get { return paused; } }
    public override bool Completed { get { return completed; } set { completed = value; } }

    //Proper scale is (90,90)

    // Start is called before the first frame update
    void Start()
    {
        adManager = GameObject.Find("AdManager").GetComponent<AdManager>();
        Difficulty = AdDifficulty.Easy;

    }

    // Update is called once per frame
    void Update()
    {
        //Scale up at beginning
        if (transform.localScale.x <= 90 && newBool == false)
        {
            ChangeScale(true);
        }

        //Scale down at end
        if (transform.localScale.x >= 30 && newBool == true)
        {
            ChangeScale(false);
        }
        //Debug.Log($"IN UPADTE METHOD: {Completed}");
        adManager.ActiveAdComplete = Completed;
        adManager.ActiveAdDifficulty = Difficulty;

        if (closeButton.isClicked)
        {
            StartCoroutine(waiter());
        }
    }

    protected override IEnumerator waiter()
    {
        yield return new WaitForSeconds(0.1f);
        newBool = true;

        yield return new WaitForSeconds(0.15f);
        yield return Completed = true;
        Debug.Log("Bye bye");
        Destroy(gameObject);
    }

    protected override IEnumerator waiterDeath()
    {
        throw new System.NotImplementedException();
    }

    public override void CreateAd()
    {
        Instantiate(gameObject);
        gameObject.SetActive(true);
    }
}
