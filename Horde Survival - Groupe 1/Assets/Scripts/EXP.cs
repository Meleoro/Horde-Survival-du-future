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
    private float moveSpeed = 5;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Collect()
    {
        Destroy(gameObject);
        OnExCollected?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public interface ICollectible
    {
        public void Collect()
        {
            Debug.Log("it works now");
        }
        
    }

    private void FixedUpdate()
    {
        if (hasTarget)
        {
            Vector2 targetDirection = targetPosition - transform.position.normalized;
            rb.velocity = new Vector2(targetDirection.x, targetDirection.y) * 5f;
        }
    }
}
