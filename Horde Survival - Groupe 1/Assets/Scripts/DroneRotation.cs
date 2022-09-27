using Character;
using Character.Projectiles;
using UnityEngine;

public class DroneRotation : MonoBehaviour
{
    [SerializeField] private Transform playerTr;
    [SerializeField] private PlayerData playerData;
    public float rotationSpeed = 30f;

    void Update()
    {
        transform.RotateAround(playerTr.position, new Vector3(0,0,1),rotationSpeed * Time.deltaTime);
        
        ObjectPooling.Instance.BasicAttack(transform.position,ObjectPooling.Instance.ShootWithMinigun(),playerData);
        
    }
    
    
}
