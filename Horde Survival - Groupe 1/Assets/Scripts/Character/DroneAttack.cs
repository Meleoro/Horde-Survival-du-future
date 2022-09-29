using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Upgrades;

public class DroneAttack : MonoBehaviour
{
    public Weapon weapon;
    public Transform tr;
    private float _nextFireTimeDrone;

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
            
            weapon.DroneShoot(tr, DroneCooldown());
        }
    }
    public bool DroneCooldown()
    {
        if(Time.time > _nextFireTimeDrone) return true;
        return false;
    }
}
