using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;

    public GameObject enemyStand;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "enemy")
            {
                TakeDamage(1);
            }
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
