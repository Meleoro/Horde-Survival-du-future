using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Upgrades;

namespace Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        [HideInInspector] public Vector2 movement;
        [HideInInspector] public Vector3 nearestEnemyPos;
        private Vector2 _aim;
        public static Vector2 PlayerPos;
        public LayerMask enemyLayer;
        [HideInInspector] public float _nextFireTime;      
        private float _radius;
        private bool _detectEnemy;
        
        #endregion

        #region Declaration
        
        [SerializeField] public Transform initialBulletPos;
        [SerializeField] private PlayerData playerData;
        public Weapon weaponUsed;
        private Rigidbody2D _rb;
        private PlayerInputActions _playerControls;
        private Transform _playerTr;
        public static PlayerController Instance;

        private GameObject _nearestEnemy;
        #endregion

        private void Awake()
        {
            _playerControls = new PlayerInputActions();
            _rb = GetComponent<Rigidbody2D>();
            _playerTr = GetComponent<Transform>();

            Instance = this;
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
            PlayerPos = _playerTr.position;
            nearestEnemyPos = EnemyNear().transform.position;
            
            //weaponUsed.Shoot(this,initialBulletPos.position);
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
        
        public bool Cooldown()
        {
            if(Time.time > _nextFireTime) return true;
            return false;
        }

        public GameObject EnemyNear()
        {
            _radius = 2;
            _detectEnemy = false;
        
            Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, _radius, enemyLayer);

            // ON CREE UN RAYCAST DE PLUS EN PLUS GRAND JUSQU'A AVOIR AU MOINS 1 ENNEMI DEDANS
            while (!_detectEnemy)
            {
                if (colliderArray.Length < 1)
                {
                    _radius += 2;

                    colliderArray = Physics2D.OverlapCircleAll(transform.position, _radius, enemyLayer);
                }

                else
                {
                    _detectEnemy = true;
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
                    _nearestEnemy = k.gameObject;
                }
            }

            return _nearestEnemy;
        }
    }
}

