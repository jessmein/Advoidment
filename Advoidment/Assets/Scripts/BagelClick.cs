using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class BagelClick : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] GameObject begal;

    private int score = 0;
    void Start()
    {
        
    }

    public void OnClick(InputValue value)
    {
        score++;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(score);
    }

}
