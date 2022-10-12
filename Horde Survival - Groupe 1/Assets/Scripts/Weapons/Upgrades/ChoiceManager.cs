using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Upgrades;
using random = UnityEngine.Random;
using Random = System.Random;

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager Instance;
    
    [Header("Lists")]
    public List<Weapon> weapons = new List<Weapon>();
    public List<Weapon> availableUpgrades = new List<Weapon>();
    public List<ButtonChoice> listButtons;
    public List<int> selectedOptions;


    [Header("Others")] public int thirdWeaponLevel;
    public int currentLevel;
    public GameObject controller;
    private int nbrWeapons;
    private bool endLoop;
    private bool notTheSame;
    private int compteur;

    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        transform.localPosition = new Vector3(0, 1000, 0);
        controller.SetActive(false);
        currentLevel = 0;

        foreach (Weapon k in weapons)
        {
            k.currentLevel = 0;

            k.levelList[currentLevel].currentAmmo = k.levelList[currentLevel].ammoMax;
            k.isReloading = false;
            k.isOnCooldown = false;
        }
        
        foreach (Weapon k in availableUpgrades)
        {
            k.currentLevel = 0;

            if (k.isWeapon)
            {
                k.levelList[currentLevel].currentAmmo = k.levelList[currentLevel].ammoMax;
                k.isReloading = false;
                k.isOnCooldown = false;
            }
        }
        
        LevelUp();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && Input.GetKey(KeyCode.P))
            LevelUp();
    }

    
    // MONTEE DE NIVEAU
    public void LevelUp()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        controller.SetActive(true);
        
        currentLevel += 1;
        Time.timeScale = 0;
        
        if (currentLevel <= 2 || currentLevel == thirdWeaponLevel)
        {
            SelectNewWeapon();
        }

        else
        {
            SelectChoices();
        }
    }

    // PROPOSE UNE NOUVELLE ARME AU JOUEUR
    public void SelectNewWeapon()
    {
        selectedOptions.Clear();
        
        // SELECTION DES TROIS CHOIX
        for (int i = 0; i < 3; i++)
        {
            if (i < weapons.Count)
            {
                listButtons[i].gameObject.SetActive(true);
                
                endLoop = false;

                while (!endLoop)
                {
                    int choice = random.Range(0, weapons.Count);
                    notTheSame = true;
                    
                    //VERIFICATION QUE LE CHOIX EST PAS DEJA PRESENT
                    foreach (int k in selectedOptions) 
                    {
                        if (choice == k)
                        {
                            notTheSame = false;
                        }
                    }
                
                
                    // L'OPTION N'EST PAS DEJA PRESENTE DANS LES CHOIX
                    if (notTheSame)
                    {
                        selectedOptions.Add(choice);
                        listButtons[i].Upgrade = weapons[choice];
                        listButtons[i].currentIndex = choice;

                        endLoop = true; 
                    }
                } 
            }

            else
            {
                listButtons[i].gameObject.SetActive(false);
            }
        }
        
        
        // ON ACTUALISE LES BOUTTONS
        foreach (ButtonChoice i in listButtons)
        {
            i.UpdateButton();
        }
    }
    
    
    // PROPOSE UNE AMELIORATION AU JOUEUR (ARMES + STATS)
    public void SelectChoices()
    {
        listButtons[2].gameObject.SetActive(true);
        
        selectedOptions.Clear();
        
        // ON CHOISIT LE NOMBRE D'ARME PRESENT DANS LES UPGRADES
        GetNrbWeapons();
        
        if (nbrWeapons > 0)
        {
            nbrWeapons = random.Range(1, nbrWeapons);
        }
        

        // SELECTION DES TROIS CHOIX
        for (int i = 0; i < 3; i++)
        {
            endLoop = false;
            compteur = 0;

            // TANT QU'ON A PAS UN NOUVEAU CHOIX VALABLE DE SELECTIONNÉ
            while (!endLoop)
            {
                int choice = random.Range(0, availableUpgrades.Count);
                notTheSame = true;

                compteur += 1;

                if (compteur >= 50)
                {
                    break;
                }

                //VERIFICATION QUE LE CHOIX EST PAS DEJA PRESENT
                foreach (int k in selectedOptions)
                {
                    if (choice == k)
                    {
                        notTheSame = false;
                    }
                }

                // L'OPTION N'EST PAS DEJA PRESENTE DANS LES CHOIX
                if (notTheSame)
                {
                    // SI ON PEUT AJOUTER UNE ARME AUX CHOIX
                    if (availableUpgrades[choice].isWeapon && nbrWeapons > 0)
                    {
                        nbrWeapons -= 1;
                        
                        selectedOptions.Add(choice);
                        listButtons[i].Upgrade = availableUpgrades[choice];

                        endLoop = true;
                        
                        listButtons[i].currentIndex = choice;
                    }
                
                    // SI ON PEUT AJOUTER UNE UPGRADE AUX CHOIX
                    else if (!availableUpgrades[choice].isWeapon && nbrWeapons == 0)
                    {
                        selectedOptions.Add(choice);
                        listButtons[i].Upgrade = availableUpgrades[choice];

                        endLoop = true;
                        
                        listButtons[i].currentIndex = choice;
                    }
                }
            }
        }
        
        // ON ACTUALISE LES BOUTTONS
        foreach (ButtonChoice i in listButtons)
        {
            i.UpdateButton();
        }
    }

    void GetNrbWeapons()
    {
        nbrWeapons = 0;
        
        foreach (Weapon k in availableUpgrades)
        {
            if (k.isWeapon)
                nbrWeapons += 1;
        }
    }
}
