using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
//using static UnityEditor.Experimental.GraphView.GraphView;
//using static UnityEngine.GraphicsBuffer;

public class RunningAd : Advertisement
{
    public GameObject player;
    public List<GameObject> enemies;
    public GameObject enemy;
    public int enemyNumber;
    private Vector3 scale;
    private bool isDead = false;
    public bool isMoving = true;

    public GameObject winScreen;
    public GameObject loseScreen;

    private System.Random rand = new System.Random();
    public AdManager adManager;
    private Vector3 originalPosition;

    private bool newBool = false;
    public override bool Paused { get { return paused; } }
    public override bool Completed { get { return completed; } set { completed = value; } }

    //Proper scale is (40,40)

    // Start is called before the first frame update
    void Start()
    {

        adManager = GameObject.Find("AdManager").GetComponent<AdManager>();
        Difficulty = AdDifficulty.Medium;

        scale = transform.localScale;
        enemy.GetComponent<SpriteRenderer>().enabled = false;
        loseScreen.GetComponent<SpriteRenderer>().enabled = false;
        winScreen.GetComponent<SpriteRenderer>().enabled = false;

        player.transform.localPosition = new Vector3(0, 0, 0);
        originalPosition = player.transform.position;
        player.transform.position = new Vector3(originalPosition.x, originalPosition.y - (5f * scale.y), 0);

        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        scale = transform.localScale;

        //Scale up at beginning
        if (transform.localScale.x <= 40 && newBool == false)
        {
            ChangeScale(true);
        }

        //Scale down at end
        if (newBool == true && transform.localScale.x >= 1)
        {
            ChangeScale(false);
        }
        //Debug.Log($"IN UPADTE METHOD: {Completed}");
        adManager.ActiveAdComplete = Completed;
        adManager.ActiveAdDifficulty = Difficulty;

        if (Paused) {
            return;
        }

        if (enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (isMoving)
                {
                    enemies[i].transform.position += new Vector3(0, (-5f * scale.x) * Time.deltaTime, 0);
                }
                
                if (enemies[i])
                {
                    //Makes the enemies offscreen invisible
                    if (enemies[i].transform.localPosition.y >= 3.45f)
                    {
                        SpriteRenderer sprender = enemies[i].GetComponent<SpriteRenderer>();
                        sprender.enabled = false;
                    }
                    if (enemies[i].transform.localPosition.y <= 3.44f)
                    {
                        SpriteRenderer sprender = enemies[i].GetComponent<SpriteRenderer>();
                        sprender.enabled = true;
                    }

                    if (player.GetComponent<Collider2D>().bounds.Intersects(enemies[i].transform.GetChild(0).gameObject.GetComponent<Collider2D>().bounds) && isDead == false)
                    {
                        Debug.Log("OUCH");
                        StartCoroutine(waiterDeath());
                    }

                    if (enemies[i].transform.localPosition.y <= -6.46f)
                    {
                        Destroy(enemies[i]);
                        enemies.RemoveAt(i);
                    }
                }
            }
        }

        if (enemies.Count == 0 && isDead == false)
        {
            StartCoroutine(waiter());
        }

        player.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, player.transform.position.y, 0);

        
        if (player.transform.localPosition.x > 2.78)
        {
            player.transform.position = new Vector3(originalPosition.x + (2.78f * scale.x), player.transform.position.y, 0);
        }

        if (player.transform.localPosition.x < -2.78)
        {
            player.transform.position = new Vector3(originalPosition.x + (-2.78f * scale.x), player.transform.position.y, 0);
        }
    }

    private void SpawnEnemies()
    {
        enemies.Clear();
        Debug.Log("Enemies!");
        float yPosition = 20 * scale.y;
        float[] points = { -2 * scale.x, 0, 2 * scale.x };

        enemies = new List<GameObject>(enemyNumber);

        for (int i = 0; i < enemyNumber; i++)
        {
            GameObject newEnemy = Instantiate(enemy);
            newEnemy.transform.localScale = scale;
            newEnemy.transform.parent = transform;
            newEnemy.transform.localPosition = Vector3.zero;

            newEnemy.transform.position = new Vector2(
                transform.position.x + points[Random.Range(0, 3)], transform.position.y + (yPosition)
                );

            enemies.Add(newEnemy);

            yPosition -= (5f * scale.y);

            
        }

        isDead = false;
        isMoving = true;
    }

    private void DeleteEnemies()
    {
        
        for (int i = 0; i < enemies.Count; i++) 
        {
            Destroy(enemies[i]);
        }
        SpawnEnemies();
    }

    protected override IEnumerator waiter()
    {
        //Completed = true;
        winScreen.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(1);
        newBool = true;

        yield return new WaitForSeconds(0.15f);
        yield return Completed = true;
        Debug.Log("See ya!");
        //gameManager.activeAds = 0;
        Destroy(gameObject);
    }

    protected override IEnumerator waiterDeath()
    {
        loseScreen.GetComponent<SpriteRenderer>().enabled = true;
        isDead = true;
        isMoving = false;
        yield return new WaitForSeconds(1);
        Debug.Log("Restarting!");
        loseScreen.GetComponent<SpriteRenderer>().enabled = false;
        DeleteEnemies();
    }

    public override void CreateAd()
    {
        Instantiate(gameObject);
        gameObject.SetActive(true);
    }

    public override void ForceCloseAd() {
        StartCoroutine(waiter());
    }
}
