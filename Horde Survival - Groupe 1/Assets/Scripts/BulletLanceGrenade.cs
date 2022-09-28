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
    public float damages;

    [Header("Grenade")] 
    public GameObject grenade;
    public int nbrRebond;
    public int limiteRebonds;
    public bool isTheOriginal;
    public bool canBounce;
    public LayerMask ennemyLayer;

    [Header("Autres")] 
    private GameObject nearestEnnemy;
    private GameObject nearestEnnemy2;
    private Vector2 direction2;
    private float radius;
    private bool detectEnnemy;
    

    private void Start()
    {
        if(canBounce)
            ChangeDirectionOpti();
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
    
    
    // SELECTIONNE UNE NOUVELLE DIRECTION POUR LE PROJECTILE
    public void ChangeDirectionOpti()
    {
        radius = 2;
        detectEnnemy = false;
        
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, radius, ennemyLayer);

        // ON CREE UN RAYCAST DE PLSU EN PLSU GRAND JUSQU'A AVOIR AU MOINS 3 ENNEMIES DEDANS
        while (!detectEnnemy)
        {
            if (colliderArray.Length < 3)
            {
                radius += 2;

                colliderArray = Physics2D.OverlapCircleAll(transform.position, radius, ennemyLayer);
            }

            else
            {
                detectEnnemy = true;
            }
        }


        // ET DANS ON TRI DANS LES ENNEMIS TROUVES DANS CE RAYCAST
        Vector2 currentPos = transform.position;
        
        float minDist = Mathf.Infinity;
        float minDist2 = Mathf.Infinity;

        foreach(Collider2D k in colliderArray)
        {
            Debug.Log(k.gameObject);
            
            float dist = Vector2.Distance(k.gameObject.transform.position, currentPos);

            if (dist < minDist && dist > 1f)
            {
                nearestEnnemy2 = nearestEnnemy;
                
                minDist = dist;
                nearestEnnemy = k.gameObject;
            }
            
            else if (dist < minDist2 && dist > 1f)
            {
                minDist2 = dist;
                nearestEnnemy2 = k.gameObject;
            }
        }
        
        direction = nearestEnnemy.transform.position - transform.position;
        direction2 = nearestEnnemy2.transform.position - transform.position;
    }


    // CREE UNE NOUVELLE GRENADE
    public void Split()
    {
        GameObject grenade2 = Instantiate(grenade, transform.position, Quaternion.identity);

        BulletLanceGrenade script = grenade2.GetComponent<BulletLanceGrenade>();
        
        script.isTheOriginal = false;
        script.canBounce = false;
        script.nbrRebond = nbrRebond;
        script.direction = direction2;
    }
    


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ennemy") && canBounce)
        {
            nbrRebond += 1;
            
            ChangeDirectionOpti();
            
            if(nbrRebond == 1)
                Split();

            if (nbrRebond >= limiteRebonds)
            {
                if (isTheOriginal)
                {
                    gameObject.SetActive(false);
                }

                else
                {
                    Destroy(gameObject);
                }
            }
        }

        else if (col.gameObject.CompareTag("Ennemy"))
        {
            canBounce = true;
        }
    }
}
