using UnityEngine;

namespace Character.Projectiles
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D _rb;

        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ennemy"))
            {
                gameObject.SetActive(false);
            }        
        }
    }
}


