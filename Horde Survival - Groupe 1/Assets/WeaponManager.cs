using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Upgrades;
using Character;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;

    public List<Weapon> currentWeapons = new List<Weapon>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
            Destroy(gameObject);
    }

    void Update()
    {
        foreach(Weapon k in currentWeapons)
        {
            Debug.Log(k.isReloading);
            
            if(!k.isReloading && !k.isOnCooldown)
                StartCoroutine(ShootCooldown(k.levelList[k.currentLevel - 1].fireRate, k));
        }
    }

    IEnumerator ShootCooldown(float cooldown, Weapon weapon)
    {
        GameObject nearestEnnemy = PlayerController.Instance.EnemyNear();
        
        weapon.Shoot(PlayerController.Instance.initialBulletPos.position, true, PlayerController.Instance, nearestEnnemy);

        weapon.levelList[weapon.currentLevel - 1].currentAmmo -= 1;

        
        // SI LE JOUEUR A VIDÉ SON CHARGEUR
        if (weapon.levelList[weapon.currentLevel - 1].currentAmmo <= 0)
        {
            weapon.isReloading = true;
            
            yield return new WaitForSeconds(weapon.levelList[weapon.currentLevel - 1].reload);

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
