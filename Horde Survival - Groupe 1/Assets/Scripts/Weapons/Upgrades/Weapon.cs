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

            if (ammoUsed != null && pc.PlayerCooldown() && pc._nearestEnemy != null)
            {            
                ammoUsed.GetComponent<Bullet>().degats = levelList[currentLevel - 1].degats;
                ammoUsed.GetComponent<Bullet>().bulletLifeTime = levelList[currentLevel - 1].portee;
                ammoUsed.GetComponent<Bullet>()._countdown = 0;
                
                //Placement & activation
                ammoUsed.transform.position = initialPos;
                ammoUsed.SetActive(true);
    
                //Physic
                //ammoUsed.GetComponent<Rigidbody2D>().velocity = (pc.nearestEnemyPos - pc.transform.position).normalized * levelList[currentLevel - 1].bulletSpeed;

                Vector2 mousePos = RefCamera.Instance.camera.ScreenToWorldPoint(pc._playerControls.Player.MousePosition.ReadValue<Vector2>());;

                //mousePos = new Vector2(mousePos.x - (mousePos.x / 2), mousePos.y - (mousePos.y / 2));
                
                ammoUsed.GetComponent<Rigidbody2D>().velocity = (mousePos - new Vector2(pc.transform.position.x, pc.transform.position.y)).normalized 
                                                                * levelList[currentLevel - 1].bulletSpeed;
                
                Debug.Log(mousePos);

                if (ammoUsed.CompareTag("BulletGrenade"))
                {
                    ammoUsed.GetComponent<BulletLanceGrenade>().direction = (pc.nearestEnemyPos - pc.transform.position).normalized * levelList[currentLevel - 1].bulletSpeed;
                    ammoUsed.GetComponent<BulletLanceGrenade>().nbrRebond = 0;
                }

                if (ammoUsed.CompareTag("BulletMiniGun"))
                {
                    RefCharacter.Instance.GetComponent<Rigidbody2D>().AddForce((-pc.nearestEnemyPos + pc.transform.position).normalized * levelList[currentLevel - 1].knockbackStrenght,
                        ForceMode2D.Impulse);
                }

                _currentAmmo -= 1;
                levelList[currentLevel - 1].currentAmmo = _currentAmmo;

                //Cooldown & Reload
                if (_currentAmmo > 0) pc.nextTimeFire = Time.time + levelList[currentLevel - 1].fireRate;
                else
                {
                    pc.nextTimeFire = Time.time + levelList[currentLevel - 1].reload;
                    _currentAmmo = levelList[currentLevel - 1].ammoMax;
                }
            }
        }
        public void DroneShoot(Transform dronePos, DroneAttack drone)
        {
            GameObject ammoUsed = ObjectPooling.Instance.GetObject(bullet.name);
            

            if (ammoUsed != null && drone.DroneCooldown() && PlayerController.Instance._nearestEnemy != null)
            {
                ammoUsed.GetComponent<Bullet>().degats = levelList[currentLevel - 1].degats;
                ammoUsed.GetComponent<Bullet>().bulletLifeTime = levelList[currentLevel - 1].portee;
                ammoUsed.GetComponent<Bullet>()._countdown = 0;
                
                //Placement & activation
                ammoUsed.transform.position = dronePos.position;
                ammoUsed.SetActive(true);

                Vector3 dir = dronePos.position - PlayerController.PlayerPos;
    
                //Physic
                ammoUsed.GetComponent<Rigidbody2D>().velocity = dir.normalized * levelList[currentLevel - 1].bulletSpeed;

                if (ammoUsed.CompareTag("BulletGrenade"))
                {
                    ammoUsed.GetComponent<BulletLanceGrenade>().direction = dir * levelList[currentLevel - 1].bulletSpeed;
                    ammoUsed.GetComponent<BulletLanceGrenade>().nbrRebond = 0;
                }

                _currentAmmo -= 1;

                //Cooldown & Reload
                if (_currentAmmo > 0) drone.nextFireTimeDrone = Time.time + levelList[currentLevel - 1].fireRate;
                else
                {
                    drone.nextFireTimeDrone = Time.time + levelList[currentLevel - 1].reload;
                    _currentAmmo = levelList[currentLevel - 1].ammoMax;
                }
            }
        }
        
        
        [Header("Upgrade / Weapon")]
        public string name;
        public Sprite image;
        public int currentLevel;
        public bool isWeapon;

        [Header("If is Weapon")] 
        public GameObject weapon;
        public GameObject bullet;
        public AudioSource audio;
        public List<Levels> levelList = new List<Levels>();

        [Header("If is Stats")] 
        public string description;
    
        public bool damages;
        public int damagesGain;
    
        public bool health;
        public int healthGain;
    
        public bool speed;
        public int speedGain;
    
        public bool droneSpd;
        public int droneSpdGain;

        public bool XPMagnet;
        public int XPMagnetGain;

        public bool XPBoost;
        public int XPBoostGain;
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
        public bool ballesPerçantes2;

        [Header("LanceGrenade")] 
        public int nbrRebonds;
        public bool firstSplit;
        public bool secondSplit;
    
        [Header("Minigun")] 
        public bool ballesPerçantes;
        public bool noRecul;
        public float knockbackStrenght;
    }
}