using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonChoice : MonoBehaviour
{
    public Upgrade Upgrade;

    public TextMeshProUGUI name;
    public TextMeshProUGUI description;

    public Image image;
    

    public void UpdateButton()
    {
        name.text = Upgrade.name;
        description.text = Upgrade.description;
    }
}
