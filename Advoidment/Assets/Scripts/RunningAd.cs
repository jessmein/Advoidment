using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class RunningAd : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    private Vector3 scale;
    private int move;
    // Start is called before the first frame update
    void Start()
    {
        enemy.transform.localPosition = Vector3.zero;
        scale = transform.localScale;
        move = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //player.transform.position += new Vector3(0, 0.001f, 0);
        if (enemy)
        {
            enemy.transform.position += new Vector3(0, -0.001f, 0);
            if (enemy.transform.localPosition.y <= -6.46f)
            {
                Destroy(enemy);
            }
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
}
