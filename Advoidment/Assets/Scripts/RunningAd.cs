using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static UnityEditor.Experimental.GraphView.GraphView;
//using static UnityEngine.GraphicsBuffer;

public class RunningAd : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> enemies;
    public GameObject enemy;
    public int enemyNumber;
    private Vector3 scale;
    private int move;
    private bool isDead = false;
    public bool isMoving = true;

    public GameObject winScreen;
    public GameObject loseScreen;

    private System.Random rand = new System.Random();
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject gObject in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (gObject.name == "GameManager")
            {
                gameManager = gObject.GetComponent<GameManager>();
            }
        }
        scale = transform.localScale;
        enemy.GetComponent<SpriteRenderer>().enabled = false;
        loseScreen.GetComponent<SpriteRenderer>().enabled = false;
        winScreen.GetComponent<SpriteRenderer>().enabled = false;

        SpawnEnemies();
        move = 0;
    }

    // Update is called once per frame
    void Update()
    {
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

                    if (player.GetComponent<Collider2D>().bounds.Intersects(enemies[i].GetComponent<Collider2D>().bounds) && isDead == false)
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
        
        if (Input.GetButtonDown("Right") && move < 1)
        {
            player.transform.position += new Vector3(2 * scale.x, 0, 0);
            move++;
        }

        if (Input.GetButtonDown("Left") && move > -1)
        {
            player.transform.position += new Vector3(-2 * scale.x, 0, 0);
            move--;
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

    IEnumerator waiter()
    {
        winScreen.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(1);
        Debug.Log("See ya!");
        gameManager.activeAds = 0;
        Destroy(gameObject);
    }

    IEnumerator waiterDeath()
    {
        loseScreen.GetComponent<SpriteRenderer>().enabled = true;
        isDead = true;
        isMoving = false;
        yield return new WaitForSeconds(1);
        Debug.Log("Restarting!");
        loseScreen.GetComponent<SpriteRenderer>().enabled = false;
        DeleteEnemies();
    }

    public void CreateAd()
    {
        Instantiate(gameObject);
        gameObject.SetActive(true);
    }
}
