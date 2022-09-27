using System;
using System.Collections;
using System.Collections.Generic;
using Character.Projectiles;
using UnityEngine;

public class CharacterControler : MonoBehaviour
{
    public static CharacterControler Instance;

    private GameObject nearestEnnemy;

    private void Awake()
    {

        if (Instance == null)
            Instance = this;
        
        else
            Destroy(gameObject);
    }


    private void Update()
    {
        
    }


    public GameObject NearestEnnemy()
    {
        Vector2 currentPos = transform.position;
        
        float minDist = Mathf.Infinity;

        foreach(GameObject k in SpawnManager.Instance.ennemies)
        {
            float dist = Vector2.Distance(k.transform.position, currentPos);

            if (dist < minDist)
            {
                minDist = dist;
                nearestEnnemy = k;
            }
        }

        return nearestEnnemy;
    }
}
