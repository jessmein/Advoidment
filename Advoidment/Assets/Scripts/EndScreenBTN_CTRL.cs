using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class EndScreenBTN_CTRL : MonoBehaviour
{
    // Start is called before the first frame update

    
    TMP_Text finalScoreText;
    void Start()
    {
        finalScoreText = GameObject.Find("FinalScoreText").GetComponent<TMP_Text>();//.GetComponent<Text>();

        Debug.Log(finalScoreText);

        finalScoreText.text = "Final Score: " + BagelClick.score;


    }

    public void ToStartScreen()
    {
        SceneManager.LoadScene("Start Screen");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Endless Mode");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
