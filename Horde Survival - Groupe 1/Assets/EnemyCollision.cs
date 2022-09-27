using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public int currentHealth;
    public PlayerHealth ph;

    public HealthBar healthBar;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        { 
            ph.TakeDamage(1); 
            Debug.Log("Encounter");
        }
    }
}

