using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class ButtonChoice : MonoBehaviour
{
    public Upgrade Upgrade;
    public int currentIndex;

    public TextMeshProUGUI buttonName;
    public TextMeshProUGUI description;

    public Image image;


    public void ButtonPressed()
    {
        Upgrade.currentLevel += 1;

        Time.timeScale = 1;

        if (ChoiceManager.Instance.currentLevel <= 2)
        {
            ChoiceManager.Instance.weapons.RemoveAt(currentIndex);
            ChoiceManager.Instance.availableUpgrades.Add(Upgrade);
        }
        
        ChoiceManager.Instance.transform.localPosition = new Vector3(0, 1000, 0);
    }

    public void UpdateButton()
    {
        buttonName.text = Upgrade.name;
        description.text = Upgrade.levelList[Upgrade.currentLevel].desciption;
    }
}
