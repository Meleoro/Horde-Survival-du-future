using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Encounter12");
        if (col.collider.CompareTag("enemy"))
        { 
            TakeDamage(1);
            Debug.Log("Encounter");
        }
    }
}
