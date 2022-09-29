using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Upgrades;

public class DroneAttack : MonoBehaviour
{
    public Weapon weapon;
    public Transform tr;
    [HideInInspector]
    public float nextFireTimeDrone;

    private void Start()
    {
        tr = GetComponent<Transform>();
    }

    private void Update()
    {
        DroneCooldown();
    }

    void FixedUpdate()
    {
        if (weapon != null)
        {
            Debug.Log(2);
            
            weapon.DroneShoot(tr, this);
        }
    }
    public bool DroneCooldown()
    {
        if(Time.time > nextFireTimeDrone) return true;
        return false;
    }
}
