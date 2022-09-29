using System;
using Unity.VisualScripting;
using UnityEngine;
using Upgrades;

namespace Character.Projectiles
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Transform _tr;
        private Vector2 _pC;
        


        private void Start()
        {
            _pC = PlayerController.PlayerPos;
            _tr = GetComponent<Transform>();
        }

        private void Update()
        {
            if (Vector2.Distance(_tr.position, _pC) > 30f)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ennemy"))
            {
                gameObject.SetActive(false);
            }        
        }
    }
}


