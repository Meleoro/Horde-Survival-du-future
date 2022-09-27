using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class Upgrade : ScriptableObject
{
    [Header("Upgrade / Weapon")]
    public string name;
    public string description;
    public Image image;
    public int currentLevel;
    public bool isWeapon;

    [Header("If is Weapon")] 
    public GameObject weapon;

    [Header("Else")] 
    public bool damages;
    public bool health;
}
