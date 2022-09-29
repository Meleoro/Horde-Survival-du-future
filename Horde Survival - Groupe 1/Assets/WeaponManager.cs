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
    public bool cooldownSwitch;

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

        /*
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
        }*/
    }


    void SwitchWeapon()
    {
        // SI ON A ACTUELLEMENT 2 ARMES QUE L'ON VEUT SWITCH
        if (currentWeapons.Count == 2)
        {
            Weapon stockage0 = currentWeapons[0];
            
            currentWeapons[0] = currentWeapons[1];
            currentWeapons[1] = stockage0;
            
            currentWeapons[0].isReloading = true;
            currentWeapons[1].isReloading = true;
            
            Debug.Log(currentWeapons[0]);
            Debug.Log(currentWeapons[1]);
            
            StartCoroutine(SwitchWeaponCooldown1());
        }
        
        // SI ON A ACTUELLEMENT 3 ARMES QUE L'ON VEUT SWITCH
        else if (currentWeapons.Count == 3)
        {
            Weapon stockage0 = currentWeapons[0];
            Weapon stockage1 = currentWeapons[1];
            
            currentWeapons[0] = currentWeapons[1];
            currentWeapons[1] = currentWeapons[2];
            currentWeapons[2] = stockage1;
            
            currentWeapons[0].isReloading = true;
            currentWeapons[1].isReloading = true;
            currentWeapons[2].isReloading = true;
            
            StartCoroutine(SwitchWeaponCooldown2());
        }
    }

    IEnumerator SwitchWeaponCooldown1()
    {
        cooldownSwitch = true;
        
        yield return new WaitForSeconds(1);
        
        currentWeapons[0].isReloading = false;
        currentWeapons[1].isReloading = false;
        
        currentWeapons[0].isOnCooldown = false;
        currentWeapons[1].isOnCooldown = false;

        currentWeapons[0].levelList[currentWeapons[0].currentLevel - 1].currentAmmo =
            currentWeapons[0].levelList[currentWeapons[0].currentLevel - 1].ammoMax;
        currentWeapons[1].levelList[currentWeapons[1].currentLevel - 1].currentAmmo =
            currentWeapons[1].levelList[currentWeapons[1].currentLevel - 1].ammoMax;
        
        cooldownSwitch = false;
    }
    
    IEnumerator SwitchWeaponCooldown2()
    {
        cooldownSwitch = true;
        
        yield return new WaitForSeconds(1);
        
        currentWeapons[0].isReloading = false;
        currentWeapons[1].isReloading = false;
        currentWeapons[2].isReloading = false;
        
        currentWeapons[0].isOnCooldown = false;
        currentWeapons[1].isOnCooldown = false;
        currentWeapons[2].isOnCooldown = false;
        
        cooldownSwitch = false;
    }
}
