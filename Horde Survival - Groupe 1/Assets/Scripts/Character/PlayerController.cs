using Character.Projectiles;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        [HideInInspector]
        public Vector2 movement;
        private Vector2 _aim;
        private float _nextFireTime;

        #endregion

        public LayerMask ennemyLayer;
        private float radius;
        private bool detectEnnemy;
        private GameObject nearestEnnemy;
        
        #region Declaration
        
        [SerializeField] private Transform initialBulletPos;
        [SerializeField] private PlayerData playerData;
        
        [SerializeField] private Levels _weaponStats;
        [SerializeField] private Upgrade weaponUsed;
        private Rigidbody2D _rb;
        private PlayerInputActions _playerControls;

        #endregion

        private void Awake()
        {
            _playerControls = new PlayerInputActions();
            _rb = GetComponent<Rigidbody2D>();
        }
        private void OnEnable()
        {
            _playerControls.Enable();
            
            _playerControls.Player.Movement.performed += OnMovement;
            _playerControls.Player.Aim.performed += OnAim;
        }
        private void OnDisable()
        {
            _playerControls.Disable();
        }
        private void Update()
        {
            weaponUsed.Shoot(initialBulletPos.position,_weaponStats,weaponUsed);
        }
        private void FixedUpdate()
        {
            HandleMovement();
            HandleRotation();
        }
        void OnMovement(InputAction.CallbackContext context)
        {
            movement = context.ReadValue<Vector2>();
        }
        void OnAim(InputAction.CallbackContext context)
        {
            _aim = context.ReadValue<Vector2>();
        }
        void HandleMovement()
        {
            _rb.velocity = new Vector2(movement.x * playerData.characterSpeed, movement.y * playerData.characterSpeed);
        }
        void HandleRotation()
        {
            float a = Mathf.Atan2(_aim.x, _aim.y) * Mathf.Rad2Deg;
            _rb.rotation = -a;
        }
        
        public GameObject EnemyNear()
        {
            radius = 2;
            detectEnnemy = false;
        
            Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, radius, ennemyLayer);

            // ON CREE UN RAYCAST DE PLSU EN PLSU GRAND JUSQU'A AVOIR AU MOINS 3 ENNEMIES DEDANS
            while (!detectEnnemy)
            {
                if (colliderArray.Length < 3)
                {
                    radius += 2;

                    colliderArray = Physics2D.OverlapCircleAll(transform.position, radius, ennemyLayer);
                }

                else
                {
                    detectEnnemy = true;
                }
            }


            // ET DANS ON TRI DANS LES ENNEMIS TROUVES DANS CE RAYCAST
            Vector2 currentPos = transform.position;
        
            float minDist = Mathf.Infinity;
            float minDist2 = Mathf.Infinity;

            foreach(Collider2D k in colliderArray)
            {
                float dist = Vector2.Distance(k.gameObject.transform.position, currentPos);

                if (dist < minDist && dist > 1f)
                {
                    minDist = dist;
                    nearestEnnemy = k.gameObject;
                }
            }

            return nearestEnnemy;
        }
    }
}

