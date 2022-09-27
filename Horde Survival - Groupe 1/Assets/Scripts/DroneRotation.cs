using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneRotation : MonoBehaviour
{
    [SerializeField] private Transform playerTr;
    public float rotationSpeed = 30f;

    void Update()
    {
        transform.RotateAround(playerTr.position, new Vector3(0,0,1),rotationSpeed * Time.deltaTime);
    }
}
