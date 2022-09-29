using UnityEngine;

namespace Enemy
{
    public class HurtZone : MonoBehaviour
    {
    
        public PlayerHealthManager other;
    
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            { 
                Debug.Log(3);
                other.TakeDamage(1);
            }
        }
    }
}

