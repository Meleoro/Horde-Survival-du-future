using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectionZone : MonoBehaviour
{
    private PlayerController _pC;

    private void Start()
    {
        _pC = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        //transform.position = transform.position +  _pC.movement;
    }
}
