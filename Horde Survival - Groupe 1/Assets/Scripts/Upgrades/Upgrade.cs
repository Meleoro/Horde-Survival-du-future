using System;
using System.Collections;
using System.Collections.Generic;using System.Security.Claims;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class Upgrade : ScriptableObject
{
    [Header("Upgrade / Weapon")]
    public string name;
    public Image image;
    public int currentLevel;
    public bool isWeapon;

    [Header("If is Weapon")] 
    public GameObject weapon;
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
    public int munitions;
    public float rechargement;

    [Header("UZI")] 
    public bool doubleUZI;
}
