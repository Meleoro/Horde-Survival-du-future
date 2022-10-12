using System;
using System.Collections;
using System.Collections.Generic;
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
        public static Vector3 PlayerPos;
        public LayerMask enemyLayer;
        private float _radius;
        public float nextTimeFire;
        private bool _detectEnemy;
        private bool invinsibilite;
        
        #endregion

        #region Declaration
        
        [SerializeField] public Transform initialBulletPos;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private PlayerHealthManager healthManager;
        [SerializeField] private SpriteRenderer spriteRen;
        public Animator anim;
        public Weapon weaponUsed;
        private Rigidbody2D _rb;
        public PlayerInputActions _playerControls;
        public Transform playerTr;
        public static PlayerController Instance;

        public GameObject _nearestEnemy;
        #endregion

        private void Awake()
        {
            _playerControls = new PlayerInputActions();
            _rb = GetComponent<Rigidbody2D>();
            playerTr = GetComponent<Transform>();

            Instance = this;
            weaponUsed.Initialize();
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

        private void Start()
        {
            _rb.drag = 10;
        }

        private void Update()
        {
            PlayerCooldown();
            PlayerPos = playerTr.position;
            //nearestEnemyPos = EnemyNear().transform.position;

            if(weaponUsed != null)
                weaponUsed.PlayerShoot(this,initialBulletPos.position);

            if (Mathf.Abs(_rb.velocity.x) > 0.1f || Mathf.Abs(_rb.velocity.y) > 0.1f)
                anim.SetBool("walk", true);

            else
                anim.SetBool("walk", false);
            
            if (_rb.velocity.x < -0.1f)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (_rb.velocity.x > 0.1f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

        }
        
        public bool PlayerCooldown()
        {
            if(Time.time > nextTimeFire) return true;
            return false;
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
            float newSpeed = playerData.characterSpeed + (playerData.characterSpeed * UpgradeManager.Instance.speedPourc / 100);
            
            //_rb.velocity = new Vector2(movement.x * newSpeed, movement.y * newSpeed);
            _rb.AddForce(new Vector2(movement.x * newSpeed, movement.y * newSpeed), ForceMode2D.Force);
        }
        void HandleRotation()
        {
            float a = Mathf.Atan2(_aim.x, _aim.y) * Mathf.Rad2Deg;
            _rb.rotation = -a;
        }

        public GameObject EnemyNear()
        {
            if (SpawnManager.Instance.compteurEnnemis != 0)
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

            else
            {
                _nearestEnemy = null;
            }
            
            return null;
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Ennemy") && !invinsibilite)
            {
                healthManager.TakeDamage(1);
                StartCoroutine(GetHit());
            }
        }

        IEnumerator GetHit()
        {
            spriteRen.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            spriteRen.color = Color.white;

        }
    }
}

