using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class ComboManager : MonoBehaviour
{
    [Header("Parameters")] 
    public float maxMultiplier;
    public float timerModifier;
    public Gradient gradient;

    [Header("Références")] 
    public TextMeshProUGUI multiplier;
    public Image slider;
    public RectTransform all;
    public RectTransform multiplierPos;
    public RectTransform sliderPos;

    [Header("Variables")] 
    public float currentMultiplier;
    private float timer;
    public static ComboManager Instance;
    private bool canShakeAgain;
    
    private Vector3 pos1;
    private Vector3 pos2;

    private Vector3 originalPosMultiplier;
    private Vector3 originalPosSlider;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        pos1 = all.position;
        pos2 = pos1 + new Vector3(10, -10, 0);

        originalPosMultiplier = multiplierPos.localPosition;
        originalPosSlider = sliderPos.localPosition;

        canShakeAgain = true;
    }


    private void Update()
    {
        currentMultiplier = Mathf.Round(currentMultiplier * 10f) * 0.1f;
        
        multiplier.text = "X " + currentMultiplier;
        slider.fillAmount = timer;

        multiplier.color = gradient.Evaluate(currentMultiplier / maxMultiplier);
        slider.color = gradient.Evaluate(currentMultiplier / maxMultiplier);

        
        // CHANGEMENT DE POSITION
        all.position = new Vector3(Mathf.Lerp(pos1.x, pos2.x, currentMultiplier / maxMultiplier),
            Mathf.Lerp(pos1.y, pos2.y, currentMultiplier / maxMultiplier), 0);

        // ROTATION
        all.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, 10, (currentMultiplier - 1)/(maxMultiplier - 1)));

        // GROSSISSEMENT
        multiplier.transform.localScale = new Vector3(Mathf.Lerp(1, 1.3f, currentMultiplier / maxMultiplier), 
            Mathf.Lerp(1, 1.3f, currentMultiplier / maxMultiplier), 1);

        if(canShakeAgain)
            StartCoroutine(Shake());


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


    IEnumerator Shake()
    {
        multiplier.transform.DOShakePosition(0.1f, currentMultiplier / 2 - 0.5f);
        
        slider.transform.DOShakePosition(0.1f, currentMultiplier / 2 - 0.5f);

        canShakeAgain = false;

        yield return new WaitForSeconds(0.1f);

        canShakeAgain = true;
    }
}
