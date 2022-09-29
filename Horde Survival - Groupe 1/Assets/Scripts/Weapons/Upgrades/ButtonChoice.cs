using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using Upgrades;

public class ButtonChoice : MonoBehaviour
{
    public Weapon Upgrade;
    public int currentIndex;

    public TextMeshProUGUI buttonName;
    public TextMeshProUGUI description;

    public Image image;


    public void ButtonPressed()
    {
        Upgrade.currentLevel += 1;

        Time.timeScale = 1;

        // SI ON AJOUTE UNE ARME AU PERSONNAGE
        if (ChoiceManager.Instance.currentLevel <= 2)
        {
            ChoiceManager.Instance.weapons.RemoveAt(currentIndex);
            ChoiceManager.Instance.availableUpgrades.Add(Upgrade);
            WeaponManager.Instance.currentWeapons.Add(Upgrade);
        }
        
        // VERIFICATION SI L'ARME A ATTEINT SON NIVEAU MAX
        else if (Upgrade.isWeapon)
        {
            if (Upgrade.currentLevel >= Upgrade.levelList.Count - 1)
            {
                ChoiceManager.Instance.availableUpgrades.RemoveAt(currentIndex);
            }
        }

        if (!Upgrade.isWeapon)
        {
            // SI LE JOUEUR AMELIORE SES DEGATS
            if (Upgrade.damages)
            {
                
            }
            
            // SI LE JOUEUR AMELIORE SES HP
            else if (Upgrade.health)
            {
                
            }
            
            // SI LE JOUEUR AMELIORE SA VITESSe
            else if (Upgrade.speed)
            {
                
            }

            // SI LE JOUEUR AMELIORE LA VITESSE DE SES DRONES
            else if (Upgrade.droneSpd)
            {
                
            }
            
            // SI LE JOUEUR AMELIORE SON AIMANT A XP
            else if (Upgrade.XPMagnet)
            {
                
            }
            
            // SI LE JOUEUR AMELIORE LE TAUX D'XP RECU
            else if (Upgrade.XPBoost)
            {
                
            }
        }
        
        ChoiceManager.Instance.transform.localPosition = new Vector3(0, 1000, 0);
    }

    public void UpdateButton()
    {
        buttonName.text = Upgrade.name;

        if (Upgrade.isWeapon)
            description.text = Upgrade.levelList[Upgrade.currentLevel].desciption;

        else
            description.text = Upgrade.description;
    }
}