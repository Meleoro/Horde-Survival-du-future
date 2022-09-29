using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    [Header("Parameters")] 
    public float maxMultiplier;
    public float timerModifier;

    [Header("Références")] 
    public TextMeshProUGUI multiplier;
    public Image slider;

    [Header("Variables")] 
    private float currentMultiplier;
    private float timer;
    public static ComboManager Instance;


    private void Awake()
    {
        Instance = this;
    }


    private void Update()
    {
        multiplier.text = "X " + currentMultiplier;
        slider.fillAmount = timer;
        
        if (timer > 0)
        {
            timer -= (Time.deltaTime * currentMultiplier) / timerModifier;
        }
        else
        {
            currentMultiplier = 1;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            IncreaeMultiplier(0.1f);
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
