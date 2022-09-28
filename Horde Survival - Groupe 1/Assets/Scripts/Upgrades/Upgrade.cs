using System;
using System.Collections.Generic;
using Character;
using Character.Projectiles;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class Upgrade : ScriptableObject
{
    private float _nextFireTime;
    private PlayerController _pC;
    
    public void Shoot(Vector2 initialPos, Levels weaponStats, Upgrade weaponData)
    {
        GameObject ammoUsed = ObjectPooling.Instance.GetObject(weaponData.bullet.name);
        if (ammoUsed != null && Cooldown())
        {
            //Placement & activation
            ammoUsed.transform.position = initialPos;
            ammoUsed.SetActive(true);
            
            //Physic
            ammoUsed.GetComponent<Rigidbody2D>().velocity = Vector2.up * weaponData.levelList[0].fireRate;
            //_pC.EnemyNear().transform.position

            //Cooldown
            _nextFireTime = Time.time + weaponStats.reload;
        }
    }
    
    bool Cooldown()
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

    [Header("Else")] 
    public bool damages;
    public bool health;
}

[Serializable]
public class Levels
{
    public string desciption;

    [Header("General")]
    public float degats;
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
