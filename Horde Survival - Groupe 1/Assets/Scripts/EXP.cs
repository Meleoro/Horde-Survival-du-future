using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EXP : Collectible, ICollectible
{
    public static event Action OnExCollected;
    Rigidbody2D rb;

    bool hasTarget;
    Vector3 targetPosition;
    private float moveSpeed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Collect()
    {
        Destroy(gameObject);
        OnExCollected?.Invoke();
    }
    
    /*public interface ICollectible
    {
        public void Collect()
        {
            Debug.Log("it works now");
        }
        
    }*/

    private void FixedUpdate()
    {
        if (hasTarget)
        {
            Vector2 targetDirection = targetPosition - transform.position;
            rb.velocity = targetDirection.normalized * moveSpeed;
        }
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true;
    }
}
