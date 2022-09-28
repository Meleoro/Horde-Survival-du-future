using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtZone : MonoBehaviour
{
    
    public PlayerHealth other;
    
    void OnCollisionEnter2D(Collision2D col)
             {
                 if (col.gameObject.CompareTag("Player"))
                 { 
                     other.TakeDamage(1);
                     gameObject.SetActive(false);
                 }
             }
}

