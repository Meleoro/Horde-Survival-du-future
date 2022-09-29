using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Upgrades;

public class DroneAttack : MonoBehaviour
{
    [SerializeField] private Transform playerTr;
    public Weapon weapon;
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
            //weapon.Shoot(this, _droneTr.position);
        }
    }
    
    public bool Cooldown()
    {
        if(Time.time > nextFireTime) return true;
        return false;
    }
}
