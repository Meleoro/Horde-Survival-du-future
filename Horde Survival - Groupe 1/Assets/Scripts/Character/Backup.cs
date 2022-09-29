using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Character;
using Character.Projectiles;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Upgrades;
using Random = UnityEngine.Random;

public class Backup : MonoBehaviour
{
    public bool backup;

    public static Backup Instance;

    public GameObject drone1;
    public GameObject drone2;

    private int compteur;

    private void Awake()
    {
        Instance = this;
    }


    void Update()
    {
        compteur = 0;
        
        if (backup)
        {
            foreach (Weapon k in WeaponManager.Instance.currentWeapons)
            {
                if (!k.isReloading && !k.isOnCooldown)
                {
                    Shoot(k);
                }

                compteur += 1;
            }
        }
    }


    void Shoot(Weapon weapon)
    {
        Levels infos = weapon.levelList[weapon.currentLevel - 1];

        for (int i = 0; i < infos.bulletsPerShot; i++)
        {
            float dispersion = Random.Range(-infos.dispersion, infos.dispersion);

            GameObject bullet;
            
            if (compteur == 1)
            {
                bullet = Instantiate(weapon.bullet, drone1.transform.position, quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, 
                    Quaternion.identity.z + dispersion));
                
                bullet.GetComponent<Bullet>().GetComponent<Rigidbody2D>().velocity = (drone1.transform.position - transform.position).normalized * infos.bulletSpeed;
            }
            
            else if (compteur == 2)
            {
                bullet = Instantiate(weapon.bullet, drone2.transform.position, quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, 
                    Quaternion.identity.z + dispersion));
                
                bullet.GetComponent<Bullet>().GetComponent<Rigidbody2D>().velocity = drone2.transform.position.normalized * infos.bulletSpeed;
            }
            
            else
            {
                bullet = Instantiate(weapon.bullet, transform.position, quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, 
                    Quaternion.identity.z + dispersion));
                
                bullet.GetComponent<Bullet>().GetComponent<Rigidbody2D>().velocity = 
                    (PlayerController.Instance.EnemyNear().transform.position - transform.position).normalized * infos.bulletSpeed;
            }

            Destroy(bullet, infos.portee);
        }
        
        

        infos.currentAmmo -= 1;

        StartCoroutine(ShootCooldown(infos.fireRate, weapon));
    }
    
    
    IEnumerator ShootCooldown(float cooldown, Weapon weapon)
    {
        // SI LE JOUEUR A VIDÉ SON CHARGEUR
        if (weapon.levelList[weapon.currentLevel - 1].currentAmmo <= 0)
        {
            weapon.isReloading = true;
            
            yield return new WaitForSeconds(weapon.levelList[weapon.currentLevel - 1].reload);

            weapon.levelList[weapon.currentLevel - 1].currentAmmo = weapon.levelList[weapon.currentLevel - 1].ammoMax;
            weapon.isReloading = false;
        }

        
        // SI IL A ENCORE DES BALLES
        else
        {
            weapon.isOnCooldown = true;

            yield return new WaitForSeconds(cooldown);

            weapon.isOnCooldown = false;
        }
    }
}
