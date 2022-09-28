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
    
    public void Shoot(Vector2 initialPos, int currentLevel, bool cooldown, PlayerController pc)
    {
        GameObject ammoUsed = ObjectPooling.Instance.GetObject(bullet.name);
        
        if (ammoUsed != null && cooldown)
        {
            //Placement & activation
            ammoUsed.transform.position = initialPos;
            ammoUsed.SetActive(true);
    
            //Physic
            ammoUsed.GetComponent<Rigidbody2D>().velocity = pc.EnemyNear().transform.position * levelList[currentLevel].fireRate;
            
            //Cooldown
            pc.nextFireTime = Time.time + levelList[currentLevel].reload;
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
