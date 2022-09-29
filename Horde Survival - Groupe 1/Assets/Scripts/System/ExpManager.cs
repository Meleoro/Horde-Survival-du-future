using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.UIElements.Experimental;

public class ExpManager : MonoBehaviour
{
    public static ExpManager Instance;
    
    public static int pointCount;
    public int levelCount = 0;

    public ExpBar other;

    //collision entre le joueur et le point d'exp
    //compteur +1

    //public Variables expLevel = Array(1, 2, 3, 4, 5);

    public List<int> levels = new List<int>();


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        other.SetStartExp();
    }

    void Update()
    {
        // CALCUL DU BONUS D'XP
        
        int currentMaxXP = levels[levelCount] - (levels[levelCount] * UpgradeManager.Instance.XPBoostXP / 100);
        
        ExpBar.Instance.slider.maxValue = currentMaxXP;

        
        // LEVEL UP
        if (ExpBar.Instance.currentXp >= currentMaxXP)
        {
            levelCount += 1;
            
            ExpBar.Instance.currentXp = 0;
            ExpBar.Instance.slider.maxValue = levels[levelCount];
            
            ChoiceManager.Instance.LevelUp();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            ExpBar.Instance.currentXp += 1;
        }
        
        other.UpdateExp();
    }
}


