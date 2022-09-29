using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Upgrades;

public class DroneAttack : MonoBehaviour
{
    [SerializeField] private Transform playerTr;
    [SerializeField] private Weapon weapon;
    private Transform _droneTr;
    
    public float rotationSpeed = 30f;
    [HideInInspector] public float nextFireTime;

    private void Start()
    {
        _droneTr = GetComponent<Transform>();
    }
    
    void FixedUpdate()
    {
        if (weapon != null)
        {
            weapon.Shoot(Cooldown(),this, _droneTr.position);
        }
    }
    
    bool Cooldown()
    {
        if(Time.time > nextFireTime) return true;
        return false;
    }
}
