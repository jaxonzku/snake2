using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{

    public int score = 0;
    private TextMeshProUGUI scoreText;
    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        RefreshUI();
    }
    private void RefreshUI()
    {
        scoreText.text = score.ToString();
    }
    public void IncrementScore(int increment)
    {
        score += increment;
        RefreshUI();
    }



    public void SetGameOverScore()
    {
        scoreText.text = "SCORE : " + score.ToString();
    }






}
