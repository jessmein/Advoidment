using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SquareAd : Advertisement
{
    private System.Random rand = new System.Random();

    public GameObject target;
    public GameObject key;

    public AdManager adManager;
    private Drag keyDragClass;

    Collider2D targetCollider, keyCollider;

    private bool newBool = false;

    public override bool Paused { get { return paused; } }
    public override bool Completed { get { return completed; } set { completed = value; } }

    //Proper scale is (100,100)

    // Start is called before the first frame update
    void Start()
    {
        adManager = GameObject.Find("AdManager").GetComponent<AdManager>();
        Difficulty = AdDifficulty.Easy;

        if (target != null)
        {
            targetCollider = target.GetComponent<Collider2D>();   
        }

        if (key != null)
        {
            keyCollider= key.GetComponent<Collider2D>();
        }

        keyDragClass = key.GetComponent<Drag>();
        //scaleChange = new Vector3(1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //Scale up at beginning
        if (transform.localScale.x <= 100 && newBool == false)
        {
            ChangeScale(true);
        }
        
        //Scale down at end
        if (newBool == true && transform.localScale.x >= 30)
        {
            ChangeScale(false);
        }
        
        //Debug.Log($"IN UPADTE METHOD: {Completed} || {completed}");
        adManager.ActiveAdComplete = Completed;
        adManager.ActiveAdDifficulty = Difficulty;

        if (Paused) {
            return;
        }

        if (keyCollider.bounds.Intersects(targetCollider.bounds) && keyDragClass.dragged)
        {
            if (key.GetComponent<Drag>().dragging == false)
            {
                key.transform.position = target.transform.position;
                StartCoroutine(waiter());

            }
            
        }
        else if (keyCollider.bounds.Intersects(targetCollider.bounds) && !keyDragClass.dragged)
        {
             target.gameObject.transform.localPosition = new Vector2(
                (float)rand.NextDouble() * (1f + 1) - 1f,
                (float)rand.NextDouble() * (1.0f + 1.0f) - 1.0f
            );
        }
    }

    public override void CreateAd() {
        Instantiate(gameObject);
        
        
        target.gameObject.transform.localPosition = new Vector2(
            (float) rand.NextDouble() * (0.8f + 0.8f) - 0.8f,
            (float) rand.NextDouble() * (1.0f + 1.0f) - 1.0f
        );
        //targetCollider.bounds.Contains(key.transform.localPosition);
        
        //key.gameObject.transform.localPosition = target.transform.localPosition * -1;
        //transform.position = new Vector3(originalPosition.x + 50f, originalPosition.y, originalPosition.z);
        /*
        key.gameObject.transform.position = new Vector2(
            GetComponent<Drag>().originalPosition.x + 50.0f,
            GetComponent<Drag>().originalPosition.y + 50.0f
            );
        */
        
        gameObject.SetActive(true);
    }

    protected override IEnumerator waiter()
    {
        
        yield return new WaitForSeconds(1);
        newBool = true;
        
        Debug.Log("BYE");
        //gameManager.activeAds = 0;
        yield return new WaitForSeconds(0.1f);
        yield return Completed = true;
        Destroy(gameObject);
    }

    protected override IEnumerator waiterDeath() {
        return null;
    }

    public override void ForceCloseAd() {
        StartCoroutine(waiter());
    }
}
