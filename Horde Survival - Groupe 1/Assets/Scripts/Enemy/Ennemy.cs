using System;
using System.Collections;
using System.Collections.Generic;
using Character.Projectiles;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class Ennemy : MonoBehaviour
{
    [Header("Paramètres")] 
    public float speed;
    public float health;
    public bool dies;
    public float explosionForce;
    public Rigidbody2D rb;

    public GameObject loot;

    [Header("Autres")] 
    public GameObject explosion;

    [Header("Hit")] public Color originalColor;
    public SpriteRenderer renderer;
    public float flashTime;

    private void Start()
    {
        originalColor = renderer.color;
    }

    void Update()
    {
        Vector2 direction = RefCharacter.Instance.transform.position - transform.position;

        if (direction.normalized.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);

            direction = new Vector2(-direction.x, direction.y);
            transform.Translate(direction.normalized * speed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.Translate(direction.normalized * speed * Time.deltaTime);
        }
    }

    // private void OnCollisionEnter2D(Collision2D col)
    // {
    //     if (col.gameObject.tag == "Player" || col.gameObject.tag == "Explosion")
    //     {
    //         health -= 1;
    //
    //         if (health <= 0)
    //         {
    //             Dies();
    //             Instantiate(loot, transform.position, transform.rotation);
    //         }
    //     }
    // }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Bullet" || other.gameObject.tag == "BulletGrenade") && !dies)
        {
            health -= other.gameObject.GetComponent<Bullet>().degats
                + (other.gameObject.GetComponent<Bullet>().degats * UpgradeManager.Instance.degatsPourc / 100);
            
            FlashRed();

            if (health <= 0)
            {
                dies = true;
                
                StartCoroutine(Dies());
                ComboManager.Instance.IncreaeMultiplier(0.2f);

                GameObject Xp = Instantiate(loot, transform.position, transform.rotation);

                Xp.GetComponent<EXP>().valeurXp = ComboManager.Instance.currentMultiplier;
            }

            if (other.GetComponent<BulletPompe>() != null || other.GetComponent<BulletLanceGrenade>() != null)
            {
                Vector2 direction = transform.position - other.transform.position;
            
                GetComponent<Rigidbody2D>().AddForce(direction.normalized * other.gameObject.GetComponent<Bullet>().degats / 10, ForceMode2D.Impulse);
            }
        }
    }


    public void FlashRed()
    {
        renderer.color = Color.red;
        Invoke("ResetColor", flashTime);
    }

    void ResetColor()
    {
        renderer.color = originalColor;
    }
    
    
    public void Damage(float damage)
    {
        if (!dies && damage != 0)
        {
            health -= damage;
        
            FlashRed();
        
            if (health <= 0)
            {
                StartCoroutine(Dies());
                ComboManager.Instance.IncreaeMultiplier(0.2f);

                GameObject Xp = Instantiate(loot, transform.position, transform.rotation);

                Xp.GetComponent<EXP>().valeurXp = ComboManager.Instance.currentMultiplier;
            }
        }
    }


    IEnumerator Dies()
    {
        explosion.SetActive(true);
        
        yield return new WaitForSeconds(0.15f);
        
        Destroy(gameObject);
    }
}
