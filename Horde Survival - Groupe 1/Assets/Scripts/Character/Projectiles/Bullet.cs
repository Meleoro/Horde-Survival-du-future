using System;
using UnityEngine;

namespace Character.Projectiles
{
    public class Bullet : MonoBehaviour
    {
        private Transform _droneTr;

        [SerializeField]
        private int bulletRange = 25;

        private void Start()
        {
            _droneTr = GetComponent<Transform>();
        }

        private void Update()
        {

            if (Vector2.Distance(_droneTr.position, PlayerController.PlayerPos) > bulletRange) 
            {
                gameObject.SetActive(false);
            }
        }
    }
}


