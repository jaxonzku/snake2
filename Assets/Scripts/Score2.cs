using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score2 : MonoBehaviour
{

    public int score2 = 0;

    private TextMeshProUGUI scoreText2;

    private void Awake()
    {
        scoreText2 = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        RefreshUI2();


    }


    public void IncrementScore2(int increment)
    {
        score2 += increment;
        RefreshUI2();
    }

    private void RefreshUI2()
    {
        scoreText2.text = score2.ToString();
    }




}
