using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI whoWins;


    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        string whowinsText = PlayerPrefs.GetString("Player");
        int draw = PlayerPrefs.GetInt("Draw", 0);

        if (draw == 1)
        {
            scoreText.text = "   DRAW";
        }
        else
        {
            scoreText.text = "SCORE : " + finalScore;
            whoWins.text = whowinsText;
        }






    }
    public void GoHome()
    {

        SceneManager.LoadScene(0);

    }



}
