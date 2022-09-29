using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class Ennemy : MonoBehaviour
{
    [Header("Paramètres")] 
    public float speed;
    public float health;
    public bool dies;
    public float explosionForce;

    public GameObject loot;

    [Header("Autres")] public CircleCollider2D explosion;
    
    void Update()
    {
        Vector2 direction = RefCharacter.Instance.transform.position - transform.position;
        
        transform.Translate(direction.normalized * speed * Time.deltaTime);

        if (dies)
            StartCoroutine(Dies());
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Explosion")
        {
            health -= 1;

            if (health <= 0)
            {
                Dies();
                Instantiate(loot, transform.position, transform.rotation);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ennemy")
        {
            Vector2 direction = transform.position - other.transform.position;
            
            other.GetComponent<Rigidbody2D>().AddForce(-direction.normalized * explosionForce, ForceMode2D.Impulse);
        }
    }


    IEnumerator Dies()
    {
        explosion.enabled = true;
        
        yield return new WaitForSeconds(0.05f);
        
        Destroy(gameObject);
    }
}
