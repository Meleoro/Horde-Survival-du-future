using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using random = UnityEngine.Random;

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager Instance;
    
    [Header("Lists")]
    public List<Upgrade> weapons = new List<Upgrade>();
    public List<Upgrade> availableUpgrades = new List<Upgrade>();
    public List<ButtonChoice> listButtons;
    public List<int> selectedOptions;


    [Header("Others")] 
    public int currentLevel;
    private bool oneWeapon;
    private bool twoWeapons;
    private bool oneUpgrade;
    private bool endLoop;
    private bool notTheSame;

    
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
        currentLevel = 0;
        
        LevelUp();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
            LevelUp();
    }

    // MONTEE DE NIVEAU
    public void LevelUp()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        
        currentLevel += 1;
        Time.timeScale = 0;
        
        if (currentLevel <= 2)
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
        
        // ON ACTUALISE LES BOUTTONS
        foreach (ButtonChoice i in listButtons)
        {
            i.UpdateButton();
        }
    }
    
    // PROPOSE UNE AMELIORATION AU JOUEUR (ARMES + STATS)
    public void SelectChoices()
    {
        selectedOptions.Clear();
        
        // SELECTION DES TROIS CHOIX
        for (int i = 0; i < 3; i++)
        {
            endLoop = false;

            // TANT QU'ON A PAS UN NOUVEAU CHOIX VALABLE DE SELECTIONNÉ
            while (!endLoop)
            {
                int choice = random.Range(0, availableUpgrades.Count);
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
                    // SI ON PEUT AJOUTER UNE ARME AUX CHOIX
                    if (availableUpgrades[choice].isWeapon && !twoWeapons)
                    {
                        // Pour dire qu'on a une arme dans les options
                        if (!oneWeapon)
                            oneWeapon = true;
                    
                        selectedOptions.Add(choice);
                        listButtons[i].Upgrade = availableUpgrades[choice];

                        endLoop = true;
                    }
                
                    // SI ON PEUT AJOUTER UNE UPGRADE AUX CHOIX
                    else if (!availableUpgrades[choice].isWeapon && !oneUpgrade)
                    {
                        oneUpgrade = true;
                    
                        selectedOptions.Add(choice);
                        listButtons[i].Upgrade = availableUpgrades[choice];

                        endLoop = true;
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
}
