using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealthManager : MonoBehaviour
{
    public int maxHealth = 10;

    public int currentHealth;

    public HealthBar healthBar;

    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        /*Cette partie du script fonctionne
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }*/
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
