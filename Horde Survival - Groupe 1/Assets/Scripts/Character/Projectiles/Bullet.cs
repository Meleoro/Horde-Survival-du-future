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
        public Weapon weapon;

        public float bulletLifeTime;
        public float _countdown;
        public float degats;

        public bool grenade;
        public bool CCBullet;

        private void Start()
        {
            _pC = PlayerController.PlayerPos;
            _tr = GetComponent<Transform>(); 
        }

        private void LateUpdate()
        {
            if (_countdown == 0)
            {
                if (CCBullet)
                {
                    GetComponent<BulletPompe>().Shoot();
                }

                gameObject.GetComponent<TrailRenderer>().Clear();
            }

            _countdown += Time.deltaTime; 
            
            if(_countdown >= bulletLifeTime) 
            {
                _countdown = 0f;
                gameObject.SetActive(false);
            }
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!weapon.levelList[weapon.currentLevel - 1].ballesPerçantes2 && !weapon.levelList[weapon.currentLevel - 1].ballesPerçantes)
            {
                if (other.gameObject.CompareTag("Ennemy") && !grenade)
                {
                    gameObject.SetActive(false);
                } 
            }
        }
    }
}


