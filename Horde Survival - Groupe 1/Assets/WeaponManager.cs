using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Upgrades;
using Character;
using Unity.VisualScripting;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    
    private PlayerInputActions _playerControls;
    private bool cooldownSwitch;

    public DroneAttack drone1;
    public DroneAttack drone2;

    public List<Weapon> currentWeapons = new List<Weapon>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
            Destroy(gameObject);
        
        _playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }


    private void Update()
    {
        // SI LE JOUEUR VEUT SWITCH
        if (_playerControls.Player.ChangeWeapon.WasPerformedThisFrame())
        {
            if (!cooldownSwitch)
            {
                SwitchWeapon();
            }
        }


        // ON ATTRIBUE LES ARMES A CHAQUE SCRIPT
        if (currentWeapons.Count == 3)
        {
            PlayerController.Instance.weaponUsed = currentWeapons[0];
            drone1.weapon = currentWeapons[1];
            drone2.weapon = currentWeapons[2];
        }
        
        else if (currentWeapons.Count == 2)
        {
            PlayerController.Instance.weaponUsed = currentWeapons[0];
            drone1.weapon = currentWeapons[1]; 
        }
        
        else if (currentWeapons.Count == 1)
        {
            PlayerController.Instance.weaponUsed = currentWeapons[0];
        }
    }


    void SwitchWeapon()
    {
        // SI ON A ACTUELLEMENT 2 ARMES QUE L'ON VEUT SWITCH
        if (currentWeapons.Count == 2)
        {
            Weapon stockage0 = currentWeapons[0];
            
            currentWeapons[0] = currentWeapons[1];
            currentWeapons[1] = stockage0;
        }
        
        // SI ON A ACTUELLEMENT 3 ARMES QUE L'ON VEUT SWITCH
        else if (currentWeapons.Count == 3)
        {
            Weapon stockage0 = currentWeapons[0];
            Weapon stockage1 = currentWeapons[1];
            
            currentWeapons[0] = currentWeapons[1];
            currentWeapons[1] = currentWeapons[2];
            currentWeapons[2] = stockage1;
        }

        StartCoroutine(SwitchWeaponCooldown());
    }

    IEnumerator SwitchWeaponCooldown()
    {
        cooldownSwitch = true;
        
        yield return new WaitForSeconds(1);
        
        cooldownSwitch = false;
    }
    

    IEnumerator ShootCooldown(float cooldown, Weapon weapon)
    {
        GameObject nearestEnnemy = PlayerController.Instance.EnemyNear();
        
        //weapon.Shoot(PlayerController.Instance.initialBulletPos.position, true, PlayerController.Instance, nearestEnnemy);

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
