using System;
using System.Collections.Generic;
using Character;
using Character.Projectiles;
using UnityEngine;
using UnityEngine.UI;

namespace Upgrades
{
    [CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
    public class Weapon : ScriptableObject
    {
        private float _nextFireTime;
        private float _endReloadTime;
        public Vector2 spawnPos;
        public bool isReloading = false;
        public bool isOnCooldown;
    
        public void Shoot(Vector2 initialPos, PlayerController pc)
        {
            GameObject ammoUsed = ObjectPooling.Instance.GetObject(bullet.name);
        
            if (ammoUsed != null && Cooldown() && !isReloading)
            {
                for (int i = 0; i <  levelList[currentLevel].ammoMax; i++)
                {
                    //Placement & activation
                    ammoUsed.transform.position = initialPos;
                    spawnPos = initialPos;
                    ammoUsed.SetActive(true);
    
                    //Physic
                    ammoUsed.GetComponent<Rigidbody2D>().velocity = (pc.nearestEnemyPos - pc.transform.position).normalized * levelList[currentLevel].bulletSpeed;
            
                    //Cooldown
                    _nextFireTime = Time.time + levelList[currentLevel].reload;
                }
                
                //Reload Time
                _endReloadTime = Time.time + levelList[currentLevel].reload;
                isReloading = true;
            }
            Reload();
        }
        public void Shoot( DroneAttack drone, Vector3 dronePos)
        {
            GameObject ammoUsed = ObjectPooling.Instance.GetObject(bullet.name);

            if (ammoUsed != null && Cooldown() && !isReloading)
            {
                for (int i = 0; i < levelList[currentLevel].ammoMax; i++)
                {
                    Debug.Log(2);
                    //Placement & activation
                    ammoUsed.transform.position = dronePos;
                    spawnPos = dronePos;
                    ammoUsed.SetActive(true);
    
                    //Physic
                    ammoUsed.GetComponent<Rigidbody2D>().velocity = dronePos.normalized * levelList[currentLevel].bulletSpeed;
            
                    //FireRate
                    _nextFireTime = Time.time + levelList[currentLevel].fireRate;
                }

                //Reload Time
                _endReloadTime = Time.time + levelList[currentLevel].reload;
                isReloading = true;
            }
            
            Reload();
        }

        void Reload()
        {
            isReloading = Time.time < _endReloadTime;
        }
        
        private bool Cooldown()
        {
            if(Time.time > _nextFireTime) return true;
            return false;
        }

        [Header("Upgrade / Weapon")]
        public string name;
        public Image image;
        public int currentLevel;
        public bool isWeapon;

        [Header("If is Weapon")] 
        public GameObject weapon;
        public GameObject bullet;
        public List<Levels> levelList = new List<Levels>();

        [Header("If is Stats")] 
        public string description;
    
        public bool damages;
        public float damagesGain;
    
        public bool health;
        public float healthGain;
    
        public bool speed;
        public float speedGain;
    
        public bool droneSpd;
        public float droneSpdGain;

        public bool XPMagnet;
        public float XPMagnetGain;

        public bool XPBoost;
        public float XPBoostGain;
    }

    [Serializable]
    public class Levels
    {
        public string desciption;

        [Header("General")]
        public float degats;

        public float bulletSpeed;
        public float portee;
        public float fireRate;
        public float bulletsPerShot;
        public int ammoMax;
        public int currentAmmo;
        public float reload;

        [Header("UZI")] 
        public bool doubleUZI;

        [Header("Canon CC")] 
        public bool doubleTir;

        [Header("LanceGrenade")] 
        public int nbrRebonds;
        public bool firstSplit;
        public bool secondSplit;
    
        [Header("Minigun")] 
        public bool ballesPerçantes;
        public bool noRecul;
    }
}