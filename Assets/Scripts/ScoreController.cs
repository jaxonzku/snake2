using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private int score = 0;
    private int increment = 5;
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




}
