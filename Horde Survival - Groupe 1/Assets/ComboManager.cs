using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [Header("Parameters")] 
    public float maxMultiplier;
    public float timerModifier;

    [Header("Variables")] 
    private float currentMultiplier;
    private float timer;


    private void Update()
    {
        if (timer > 0)
        {
            timer -= (Time.deltaTime * currentMultiplier) / timerModifier;
        }
        else
        {
            currentMultiplier = 0;
        }
    }


    public void IncreaeMultiplier(float increaseValue)
    {
        currentMultiplier += increaseValue;
        
        if(currentMultiplier > maxMultiplier)
            currentMultiplier = maxMultiplier;

        timer = 1;
    }
}
