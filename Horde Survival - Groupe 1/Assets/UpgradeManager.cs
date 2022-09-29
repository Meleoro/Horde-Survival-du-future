using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    public int degatsPourc;
    public int healthGain;
    public int speedPourc;
    public int speedDronePourc;
    public int XPMagnetPourc;
    public int XPBoostXP;

    private void Awake()
    {
        Instance = this;
    }

}
