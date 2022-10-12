using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class CursorPlacement : MonoBehaviour
{

    [SerializeField] private PlayerController pc;


    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = RefCamera.Instance.camera.ScreenToWorldPoint(pc._playerControls.Player.MousePosition.ReadValue<Vector2>());;
        transform.position = mousePos;
    }
}
