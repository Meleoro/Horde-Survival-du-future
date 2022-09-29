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
            if(!k.isReloading)
                StartCoroutine(ShootCooldown(k.levelList[k.currentLevel - 1].fireRate, k));
        }
    }

    IEnumerator ShootCooldown(float cooldown, Weapon weapon)
    {
        weapon.Shoot(PlayerController.Instance.initialBulletPos.position, true, PlayerController.Instance);
        weapon.isReloading = true;

        yield return new WaitForSeconds(cooldown);

        weapon.isReloading = false;
    }
}
