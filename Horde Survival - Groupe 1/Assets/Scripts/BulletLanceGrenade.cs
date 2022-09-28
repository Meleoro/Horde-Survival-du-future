using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BulletLanceGrenade : MonoBehaviour
{
    public Rigidbody2D rb;
    
    [Header("General")]
    public float bulletSpeed;
    public Vector2 direction;

    [Header("Grenade")] 
    public GameObject grenade;
    public int nbrRebond;
    public int limiteRebonds;
    public bool isTheOriginal;

    [Header("Autres")] 
    private GameObject nearestEnnemy;
    private GameObject nearestEnnemy2;
    private Vector2 direction2;
    

    private void Start()
    {
        if(isTheOriginal)
            ChangeDirection();
    }

    private void Update()
    {
        rb.velocity = direction.normalized * bulletSpeed;
    }


    // SELECTIONNE UNE NOUVELLE DIRECTION POUR LE PROJECTILE
    public void ChangeDirection()
    {
        Vector2 currentPos = transform.position;
        
        float minDist = Mathf.Infinity;

        foreach(GameObject k in SpawnManager.Instance.ennemies)
        {
            float dist = Vector2.Distance(k.transform.position, currentPos);

            if (dist < minDist && dist > 1)
            {
                nearestEnnemy2 = nearestEnnemy;
                
                minDist = dist;
                nearestEnnemy = k;
            }
        }
        
        direction = nearestEnnemy.transform.position - transform.position;
        direction2 = nearestEnnemy2.transform.position - transform.position;
    }


    // CREE UNE NOUVELLE GRENADE
    public void Split()
    {
        GameObject grenade2 = Instantiate(grenade, transform.position, Quaternion.identity);

        grenade2.GetComponent<BulletLanceGrenade>().isTheOriginal = false;
        grenade2.GetComponent<BulletLanceGrenade>().nbrRebond = nbrRebond;
        grenade2.GetComponent<BulletLanceGrenade>().direction = direction2;
    }
    


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ennemy") && isTheOriginal)
        {
            nbrRebond += 1;
            
            ChangeDirection();
            
            if(nbrRebond == 1)
                Split();
        }

        else if (col.gameObject.CompareTag("Ennemy"))
        {
            isTheOriginal = true;
        }
    }
}
