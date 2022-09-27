using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using random = UnityEngine.Random;

public class ChoiceManager : MonoBehaviour
{
    [Header("Lists")]
    public List<Upgrade> weapons = new List<Upgrade>();
    public List<Upgrade> availableUpgrades = new List<Upgrade>();
    public List<ButtonChoice> listButtons;
    public List<int> selectedOptions;


    [Header("Others")] 
    private bool oneWeapon;
    private bool twoWeapons;
    private bool oneUpgrade;
    private bool endLoop;
    private bool notTheSame;


    private void Start()
    {
        SelectChoices();
    }


    public void SelectNewWeapon()
    {
        
    }
    
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
