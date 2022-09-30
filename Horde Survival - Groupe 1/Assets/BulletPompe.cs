using System.Collections;
using System.Collections.Generic;
using Character.Projectiles;
using UnityEngine;
using Upgrades;

public class BulletPompe : MonoBehaviour
{
    public Weapon weapon;

    public void Shoot()
    {
        float currentAngle = Vector3.Angle(GetComponent<Rigidbody2D>().velocity, Vector3.right);

        if (GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            currentAngle = -currentAngle;
        }

        Debug.Log(currentAngle);
        
        DuplicateBullet(currentAngle + 10);
        DuplicateBullet(currentAngle - 10);
    }
    
    
    void DuplicateBullet(float angle)
    {
        GameObject newBullet = Instantiate(gameObject,transform.position, Quaternion.identity);

        var direction = (Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.left);

        newBullet.GetComponent<Rigidbody2D>().velocity = -direction * weapon.levelList[weapon.currentLevel - 1].bulletSpeed;
        newBullet.GetComponent<Bullet>().CCBullet = false;
    }
}
