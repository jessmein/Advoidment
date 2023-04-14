using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class SkipAdUI : MonoBehaviour
{
    public Image image;
    public Text text;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        image.color = Color.gray;
        text.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.skipAd.NumSkips > 0) {
            image.color = Color.white;
        } else {
            image.color = Color.gray;
        }

        text.text = "" + gameManager.skipAd.NumSkips;
    }
}
