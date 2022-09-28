using UnityEngine;

namespace Character.Projectiles
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 _dronePos;
        private PlayerController _pC;


        private void Start()
        {
            _dronePos = transform.position;
        }

        private void Update()
        {
            Debug.Log(Vector3.Distance(_pC.playerPos, _dronePos));
            if (Vector3.Distance(_pC.playerPos, _dronePos) < 8f) 
            {
                gameObject.SetActive(false);
            }
        }
    }
}


