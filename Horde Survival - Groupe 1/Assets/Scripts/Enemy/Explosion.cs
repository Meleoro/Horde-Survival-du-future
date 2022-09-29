using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionForce;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ennemy")
        {
            Vector2 direction = transform.position - col.transform.position;
            
            col.GetComponent<Rigidbody2D>().AddForce(-direction * explosionForce, ForceMode2D.Impulse);
            
            col.GetComponent<Ennemy>().Damage(explosionForce * 10);
        }
    }
}
