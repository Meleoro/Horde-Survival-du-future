using System;
using System.Collections.Generic;
using Character;
using Character.Projectiles;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Upgrades
{

    [CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
    public class Weapon : ScriptableObject
    {
        private float _endReloadTime;
        public bool isReloading;
        public bool isOnCooldown;
        private float _nextFireTimePlayer;
        private float _nextFireTimeDrone;
        private int _currentAmmo;

        public void Initialize()
        {
            _currentAmmo = levelList[currentLevel].ammoMax;
            _nextFireTimeDrone = 0f;
            _nextFireTimePlayer = 0f;
        }

        public void PlayerShoot( PlayerController pc, Vector2 initialPos)
        {
            GameObject ammoUsed = ObjectPooling.Instance.GetObject(bullet.name);

            if (ammoUsed != null && pc.PlayerCooldown())
            {
                Debug.Log(4);
                //Placement & activation
                ammoUsed.transform.position = initialPos;
                ammoUsed.SetActive(true);
    
                //Physic
                ammoUsed.GetComponent<Rigidbody2D>().velocity = (pc.nearestEnemyPos - pc.transform.position).normalized * levelList[currentLevel].bulletSpeed;

                _currentAmmo -= 1;
                
                //Cooldown & Reload
                if(_currentAmmo > 0) pc.nextTimeFire = Time.time + levelList[currentLevel].fireRate;
                else
                {
                    pc.nextTimeFire = Time.time + levelList[currentLevel].reload;
                    _currentAmmo = levelList[currentLevel].ammoMax;
                }
            }
        }
        public void DroneShoot(Transform dronePos, DroneAttack drone)
        {
            GameObject ammoUsed = ObjectPooling.Instance.GetObject(bullet.name);

            if (ammoUsed != null && drone.DroneCooldown())
            {
                Debug.Log(dronePos);
                //Placement & activation
                ammoUsed.transform.position = dronePos.position;
                ammoUsed.SetActive(true);

                Vector3 dir = dronePos.position - PlayerController.PlayerPos;
    
                //Physic
                ammoUsed.GetComponent<Rigidbody2D>().velocity = dir * levelList[currentLevel].bulletSpeed;
                
                //Cooldown & Reload
                if(_currentAmmo > 0) drone.nextFireTimeDrone = Time.time + levelList[currentLevel].fireRate;
                else
                {
                    drone.nextFireTimeDrone = Time.time + levelList[currentLevel].reload;
                    _currentAmmo = levelList[currentLevel].ammoMax;
                }
            }
            
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
        public float dispersion;
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