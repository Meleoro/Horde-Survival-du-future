using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefCharacter : MonoBehaviour
{
    public static RefCharacter Instance;

    private void Awake()
    {

        Instance = this;
    }
}
