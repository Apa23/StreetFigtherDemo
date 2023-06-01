using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamageable
{
    // Damageable Interface attributes
    [field: SerializeField]
    public int TotalHealthPoints { get; private set; }
    public int HealthPoints { get; private set; }

    // Movement aux variables
    private bool _canJump = false;

    // Attack aux variables
    [SerializeField]
    private int _maxCombo = 0;
    private int _comboCount = 0;
    private float _lastAttack = 0;
    private float _maxComboDelay = 0.8f;


    // Attack point reference
    [SerializeField]
    private Transform _attackPoint;
    [SerializeField]
    private float _attackRange = 0.5f;
    public LayerMask enemyLayer;

    // Input Interface attributes
    private PlayerControls _playerControls;
    private Vector2 _move;
    private Vector2 _dash;

    // Animator component
    [Header("Animation")]
    private Animator _animator;

    // Rigidbody component
    private Rigidbody2D _rb2D;


    private void Awake()
    {
        // Set up input controller
        _playerControls = new PlayerControls();

        if (gameObject.name == "Player1")
        {

            // Move input and callbacks
            _playerControls.Gameplay.Move.performed += ctx => CheckMove(ctx);
            _playerControls.Gameplay.Move.canceled += ctx => _move = Vector2.zero;

            // Dash input and callbacks
            _playerControls.Gameplay.DashForward.performed += ctx => CheckDashForward();
            _playerControls.Gameplay.DashBackward.performed += ctx => CheckDashBackward();
            _playerControls.Gameplay.DashForward.canceled += ctx => _dash = Vector2.zero;
            _playerControls.Gameplay.DashBackward.canceled += ctx => _dash = Vector2.zero;

            // Jump input
            _playerControls.Gameplay.Jump.performed += ctx => CheckJump();

            // Attack input
            _playerControls.Gameplay.Attack.performed += ctx => CheckAttack();
        }
        else if (gameObject.name == "Player2")
        {

            // Move input and callbacks
            _playerControls.Gameplay2.Move.performed += ctx => CheckMove(ctx);
            _playerControls.Gameplay2.Move.canceled += ctx => _move = Vector2.zero;

            // Dash input and callbacks
            _playerControls.Gameplay2.DashForward.performed += ctx => CheckDashForward();
            _playerControls.Gameplay2.DashBackward.performed += ctx => CheckDashBackward();
            _playerControls.Gameplay2.DashForward.canceled += ctx => _dash = Vector2.zero;
            _playerControls.Gameplay2.DashBackward.canceled += ctx => _dash = Vector2.zero;

            // Jump input
            _playerControls.Gameplay2.Jump.performed += ctx => CheckJump();

            // Attack input
            _playerControls.Gameplay2.Attack.performed += ctx => CheckAttack();
        }

    }
    private void Start()
    {
        // Setting up atributes
        HealthPoints = TotalHealthPoints;
        _animator = GetComponent<Animator>();
        _rb2D = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.time - _lastAttack > _maxComboDelay)
        {
            _comboCount = 0;
            _animator.SetBool("IsAttacking", false);
            _animator.SetInteger("ComboAttack", _comboCount);

        }
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && _animator.GetCurrentAnimatorStateInfo(0).IsName("HitAnimation"))
        {
            _animator.SetBool("IsHited", false);

        }
    }

    private void FixedUpdate()
    {
        _animator.SetFloat("JumpVelocity", _rb2D.velocity.y); //Update the jump velocity in the animator
        if (_canJump)
        {
            // Performs jump
            ActionJump();
        }
        ActionMove();
        ActionDash();

    }

    private void OnEnable() //Enable input controls
    {

        _playerControls.Gameplay.Enable();

        _playerControls.Gameplay2.Enable();

    }

    private void OnDisable() //Disable input controls
    {
        _playerControls.Gameplay.Disable();
        _playerControls.Gameplay2.Disable();
    }


    private void CheckMove(InputAction.CallbackContext ctx) //Detect movement and activate the animation
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1Animation"))
        {
            _move = ctx.ReadValue<Vector2>();
        }
    }

    private void ActionMove()// Move player on X axis when the input si higher than 0 and actives/stop the animation
    {
        Vector2 dir = new Vector2(_move.x * 2, 0) * Time.deltaTime;
        transform.Translate(dir, Space.World);
        _animator.SetFloat("Speed", Mathf.Abs(dir.x));
    }

    private void CheckDashForward()// Move player on X axis when the input si higher than 0 and actives/stop the animation
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1Animation"))
        {
            _animator.SetBool("Dash", true);
            _dash = new Vector2(transform.position.x + 5, 0) * Time.deltaTime;
        }
    }
    private void CheckDashBackward()// Move player on X axis when the input si higher than 0 and actives/stop the animation
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1Animation"))
        {
            _animator.SetBool("Dash", true);
            _dash = new Vector2(transform.position.x - 5, 0) * Time.deltaTime;
        }
    }

    private void ActionDash()// Move player on X axis when the input si higher than 0 and actives/stop the animation
    {
        transform.Translate(_dash, Space.World);
    }

    private void DashEnd()
    {
        _dash = Vector2.zero;
        _animator.SetBool("Dash", false);
    }

    private void CheckJump() //Detect jump and active the animation
    {
        if (_animator.GetBool("IsJumping") == false)
        {
            _canJump = true;
            _animator.SetBool(("IsJumping"), true);
        }
    }

    private void ActionJump()// Move player on Y axis
    {
        _canJump = false;
        _rb2D.AddForce(new Vector2(0f, 300));
    }


    private void CheckAttack() //Detect attack and active the animation
    {
        if (_animator.GetBool("IsAttacking") == false)
        {
            _lastAttack = Time.time;
            _comboCount++;
            _animator.SetBool("IsAttacking", true);
            _animator.SetInteger("ComboAttack", _comboCount);
            _move = Vector2.zero;
        }
        else
        {
            _comboCount++;
            if (_comboCount > _maxCombo)
            {
                return;
            }
            if (_comboCount >= 2 && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f)
            {
                _lastAttack = Time.time;
                _comboCount = Mathf.Clamp(_comboCount, 0, _maxCombo);
                _animator.SetInteger("ComboAttack", _comboCount);
            }
        }
    }

    private void ActionAttack()
    {
        Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemys)
        {
            if (enemy.transform.TryGetComponent(out IDamageable targetHit))
            {
                targetHit.TakeHit();
            }
        }
    }

    public void TakeHit() // Decrese the health points when hitted
    {
        if (HealthPoints <= 0)
        {
            return;
        }
        if (!_animator.GetBool("IsHited"))
        {
            _animator.SetBool("IsHited", true);
        }

        HealthPoints--;
        _animator.SetInteger("HP", HealthPoints);
        if (HealthPoints <= 0)
        {

            _playerControls.Gameplay.Disable();
            _playerControls.Gameplay2.Disable();
            return;
        }

    }

    public void EndHit()
    {
        _animator.SetBool("IsHited", false);
    }


    private void OnCollisionEnter2D(Collision2D other) //Detect collision
    {
        if (other.collider.CompareTag("Floor")) // When the player collides with the floor
        {
            _animator.SetBool("IsJumping", false);// Stop jump animation
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null) return;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}