using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score1 : MonoBehaviour
{
    public int score1 = 0;

    private TextMeshProUGUI scoreText1;


    private void Awake()
    {
        scoreText1 = GetComponent<TextMeshProUGUI>();

    }

    private void Start()
    {
        RefreshUI1();


    }

    public void IncrementScore1(int increment)
    {
        score1 += increment;
        RefreshUI1();
    }

    private void RefreshUI1()
    {
        scoreText1.text = score1.ToString();
    }





}
