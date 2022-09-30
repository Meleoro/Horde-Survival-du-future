using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private void Awake()
    {
        Instance = this;
    }


    public int score;
    public TextMeshProUGUI textScore;

    public void CalculateScore()
    {
        score = score + SpawnManager.Instance.minutes * 60 + SpawnManager.Instance.seconds;

        textScore.text = "Score : " + score;
    }
    
    
    public void AddPoint(int points)
    {
        score += points;
    }
}
