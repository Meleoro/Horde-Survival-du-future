using Character.Projectiles;
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
            //Shoot With Uzi
            ObjectPooling.Instance.BasicAttack(initialBulletPos.position,ObjectPooling.Instance.ShootWithUzi(),playerData);
        }
        private void FixedUpdate()
        {
            HandleMovement();
            HandleRotation();
        }
        
        void OnMovement(InputAction.CallbackContext context)
        {
            _movement = context.ReadValue<Vector2>();
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
        
    }
}

