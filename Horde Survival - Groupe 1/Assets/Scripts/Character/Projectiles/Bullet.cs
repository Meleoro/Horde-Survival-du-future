using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Upgrades;

namespace Character.Projectiles
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Transform _tr;
        private Vector2 _pC;

        public float bulletLifeTime;
        private float _countdown;
        public float degats;

        public bool grenade;

        private void Start()
        {
            _pC = PlayerController.PlayerPos;
            _tr = GetComponent<Transform>(); 
        }

        private void Update()
        {

            _countdown += Time.deltaTime; 
            
            if(_countdown >= bulletLifeTime) 
            {
                _countdown = 0f;
                gameObject.SetActive(false);
            }
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ennemy") && !grenade)
            {
                gameObject.SetActive(false);
            }        
        }
    }
}


