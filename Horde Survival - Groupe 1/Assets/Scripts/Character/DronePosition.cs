using UnityEngine;
using Upgrades;

public class DronePosition : MonoBehaviour
{
    [SerializeField] private Transform playerTr;
   
    public float rotationSpeed = 30f;
    
    void FixedUpdate()
    {
        float newSpeed = rotationSpeed + (rotationSpeed * UpgradeManager.Instance.speedDronePourc / 100);
        
        //position Drone
        transform.position = playerTr.position;
        //Rotation Drone
        transform.Rotate(new Vector3(0,0,newSpeed)* Time.deltaTime);
    }
}
