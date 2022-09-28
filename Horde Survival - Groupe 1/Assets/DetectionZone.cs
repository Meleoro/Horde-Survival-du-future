using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectionZone : MonoBehaviour
{
    private PlayerController _pC;
    private Transform _tr;

    private void Start()
    {
        _pC = GetComponentInParent<PlayerController>();
        _tr = GetComponent<Transform>();
    }

    private void Update()
    {
        _tr.position = new Vector2(transform.position.x + _pC.movement.x, _tr.position.y + _pC.movement.y);
    }
}
