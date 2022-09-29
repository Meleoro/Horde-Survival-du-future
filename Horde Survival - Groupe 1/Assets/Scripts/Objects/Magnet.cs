using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float radius;
    public CircleCollider2D magnet;

    private void Update()
    {
        float newRadius = radius + (radius * UpgradeManager.Instance.XPMagnetPourc / 100);
        
        magnet.radius = newRadius;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EXP exp))
        {
            exp.SetTarget(transform.parent.position);
        }
    }
}
