using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// used to delete the points
public class Point : MonoBehaviour
{
    [HideInInspector]
    public Text pointGiven;

    // Start is called before the first frame update
    void Start()
    {
        pointGiven = this.GetComponent<Text>();
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
