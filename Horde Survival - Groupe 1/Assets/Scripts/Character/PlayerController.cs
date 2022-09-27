using Objects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        private Vector2 _movement;
        private Vector2 _aim;

        [SerializeField]
        private Transform initialBulletPos;
        
        private float _nextFireTime;
        
        public bool isAttacking;
        
        #endregion
        
        #region Declaration
        
        [SerializeField] private PlayerData playerData;
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
        }
        private void OnDisable()
        {
            _playerControls.Disable();
        }
        private void Update()
        {
            BasicAttackCooldown();
            BasicAttack();
        }
        private void FixedUpdate()
        {
            HandleMovement();
            HandleRotation();
        }
        void BasicAttack()
        {
            GameObject bullet = ObjectPooling.instance.GetPooledObject();
            
            if (bullet != null && BasicAttackCooldown())
            {
                Debug.Log(4);
                bullet.transform.position = initialBulletPos.position;
                bullet.SetActive(true);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * playerData.basicAttackSpeed;
                _nextFireTime = Time.time + playerData.basicAttackCooldown;
            }
        }
        void OnMovement(InputAction.CallbackContext context)
        {
            _movement = context.ReadValue<Vector2>();
            Debug.Log(_movement);
        }
        void HandleMovement()
        {
            _rb.velocity = new Vector2(_movement.x * playerData.characterSpeed, _movement.y * playerData.characterSpeed);
        }
        void HandleRotation()
        {
            float a = Mathf.Atan2(_aim.x, _aim.y) * Mathf.Rad2Deg;
            _rb.MoveRotation(-a);
        }
        bool BasicAttackCooldown()
        {
            if(Time.time > _nextFireTime) return true;
            return false;
        }

    }
}

