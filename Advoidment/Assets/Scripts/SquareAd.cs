using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SquareAd : MonoBehaviour
{
    private System.Random rand = new System.Random();

    public GameObject target;
    public GameObject key;


    public GameManager gameManager;
    private Drag keyDragClass;

    Collider2D targetCollider, keyCollider;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject gObject in SceneManager.GetActiveScene().GetRootGameObjects()) {
            if (gObject.name == "GameManager") {
                gameManager = gObject.GetComponent<GameManager>();
            }
        }

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

    public void CreateAd() {
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

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("BYE");
        gameManager.activeAds = 0;
        Destroy(gameObject);
    }
}
