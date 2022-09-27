using Character.Projectiles;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        public Vector2 movement;
        private Vector2 _aim;
        private float _nextFireTime;

        #endregion
        
        #region Declaration
        
        [SerializeField] private Transform initialBulletPos;
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
            Shoot(initialBulletPos.position,ObjectPooling.Instance.ShootWithUzi(),playerData);
        }
        private void FixedUpdate()
        {
            HandleMovement();
            HandleRotation();
        }
        private void Shoot(Vector2 initialPos, GameObject ammoUsed, PlayerData weaponData)
        {
            if (ammoUsed != null && Cooldown())
            {
                //Placement & activation
                ammoUsed.transform.position = initialPos;
                ammoUsed.SetActive(true);
                
                //Physic
                ammoUsed.GetComponent<Rigidbody2D>().velocity = Vector2.up * weaponData.basicAttackSpeed;
                
                //Cooldown
                _nextFireTime = Time.time + weaponData.basicAttackCooldown;
            }
        }
        void OnMovement(InputAction.CallbackContext context)
        {
            movement = context.ReadValue<Vector2>();
        }
        void HandleMovement()
        {
            _rb.velocity = new Vector2(movement.x * playerData.characterSpeed, movement.y * playerData.characterSpeed);
        }
        void HandleRotation()
        {
            float a = Mathf.Atan2(_aim.x, _aim.y) * Mathf.Rad2Deg;
            _rb.MoveRotation(-a);
        }
        bool Cooldown()
        {
            if(Time.time > _nextFireTime) return true;
            return false;
        }
        
    }
}

