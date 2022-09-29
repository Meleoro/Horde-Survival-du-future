using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public static ExpBar Instance;
    
    public Slider slider;
    public Gradient grad;
    public Image fill;

    public int currentXp;

    private void Awake()
    {
        Instance = this;
    }


    public void SetStartExp()
    {
        currentXp = 0;
        slider.maxValue = 10;
        slider.value = 0;

        fill.color = grad.Evaluate(1f);
    }

    public void UpdateExp()
    {
        slider.value = currentXp;
        fill.color = grad.Evaluate(slider.normalizedValue);
    }
}
