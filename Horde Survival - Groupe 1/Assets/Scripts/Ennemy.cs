using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    [Header("Paramètres")] 
    public float speed;
    public float health;
    
    void Update()
    {
        Vector2 direction = RefCharacter.Instance.transform.position - transform.position;
        
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
}
