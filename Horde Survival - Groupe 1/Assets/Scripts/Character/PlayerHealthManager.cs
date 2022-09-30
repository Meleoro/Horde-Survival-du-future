using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealthManager : MonoBehaviour
{
    public static PlayerHealthManager Instance;
    
    public int maxHealth = 10;

    public int currentHealth;

    public HealthBar healthBar;

    public GameObject player;

    public GameObject gameOverPanel;

    public GameObject controller;

    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        gameOverPanel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        /*Cette partie du script fonctionne
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }*/
        
        

        if (currentHealth <= 0)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            controller.SetActive(true);
            
            ScoreManager.Instance.CalculateScore();
        }
        
        healthBar.SetHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void IncreaseHealth()
    {
        int saveHealth = maxHealth;

        maxHealth += (maxHealth * UpgradeManager.Instance.healthGain / 100);
        
        currentHealth += (saveHealth * UpgradeManager.Instance.healthGain / 100);
    }
}
