using Character;
using Character.Projectiles;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField] private Transform playerTr;
    [SerializeField] private PlayerData weaponData;
    public float rotationSpeed = 30f;
    private float _nextFireTime;

    void Update()
    {
        transform.RotateAround(playerTr.position, new Vector3(0,0,1),rotationSpeed * Time.deltaTime);
        
        Shoot(transform.position,ObjectPooling.Instance.ShootWithMinigun(),weaponData);
    }
    
    private void Shoot(Vector2 initialPos, GameObject ammoUsed, PlayerData weaponData)
    {
        if (ammoUsed != null && Cooldown())
        {
            //Placement & activation
            ammoUsed.transform.position = initialPos;
            ammoUsed.SetActive(true);
            
            //Physic
            ammoUsed.GetComponent<Rigidbody2D>().velocity = ammoUsed.transform.localPosition * weaponData.basicAttackSpeed;
            
            //Cooldown
            _nextFireTime = Time.time + weaponData.basicAttackCooldown;
        }
    }
    
    bool Cooldown()
    {
        if(Time.time > _nextFireTime) return true;
        return false;
    }
}
