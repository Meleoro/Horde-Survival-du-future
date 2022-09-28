using UnityEngine;
using Upgrades;

public class DronePosition : MonoBehaviour
{
    [SerializeField] private Transform playerTr;
   
    public float rotationSpeed = 30f;
    
    void FixedUpdate()
    {
        //position Drone
        transform.position = playerTr.position;
        //Rotation Drone
        transform.Rotate(new Vector3(0,0,rotationSpeed)* Time.deltaTime);
    }
}
