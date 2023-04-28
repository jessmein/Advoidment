using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class SceneBTN_CTRL : MonoBehaviour
{
    // Start is called before the first frame update

    
    TMP_Text finalScoreText;
    void Start()
    {
       Scene activeScene =  SceneManager.GetActiveScene();

        if (activeScene.name == "End Screen")
        {
            finalScoreText = GameObject.Find("FinalScoreText").GetComponent<TMP_Text>();//.GetComponent<Text>();

            Debug.Log(finalScoreText);

            finalScoreText.text = "Final Score: " + BagelClick.score;
        }

    }

    public void ToStartScreen()
    {
        SceneManager.LoadScene("Start Screen");
    }

    public void StartGame()
    {
        BagelClick.score = 0;
        SceneManager.LoadScene("Endless Mode");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Quit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
