using System.Collections;
using System.Collections.Generic;
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

    public override bool Paused { get { return paused; } }
    public override bool Completed { get { return completed; } set { completed = value; } }

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
    }

    // Update is called once per frame
    void Update()
    {
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
                (float)rand.NextDouble() * (1.6f + 1.6f) - 1.6f,
                (float)rand.NextDouble() * (1.0f + 1.0f) - 1.0f
            );
        }
    }

    public override void CreateAd() {
        Instantiate(gameObject);
        
        target.gameObject.transform.localPosition = new Vector2(
            (float) rand.NextDouble() * (1.6f + 1.6f) - 1.6f,
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
        yield return Completed = true;
        Debug.Log("BYE");
        //gameManager.activeAds = 0;
        Destroy(gameObject);
    }

    protected override IEnumerator waiterDeath() {
        return null;
    }
}
