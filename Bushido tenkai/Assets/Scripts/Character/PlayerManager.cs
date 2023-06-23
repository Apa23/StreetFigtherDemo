using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour, IDamageable
{
    public CharacterConfig Config => _config;
    private CharacterConfig _config;

    private EnemyReference _enemy;

    // Damageable Interface attributes
    public int HealthPoints { get; private set; }

    // Movement aux variables
    private bool _canJump = false;

    // Attack aux variables
    private int _maxCombo;
    private int _comboCount;
    private float _lastAttack;
    private float _maxComboDelay = 0.8f;
    private int _attackDamage;
    public LayerMask enemyLayer;
    private bool _righttoLeft = false;

    // Attack point reference
    [SerializeField]
    private Transform _attackPoint;
    private float _attackRange;
    [SerializeField]
    private GameObject _shootingPoint;

    // Input Interface attributes
    private PlayerControls _playerControls;
    private Vector2 _move;

    // Dash aux variables
    private float _dashPower;
    private float _dashingTime = 0.2f;
    private float _dashingCooldown = 1f;

    // Animator component
    [Header("Animation")]
    private Animator _animator;

    // Rigidbody component
    private Rigidbody2D _rb2D;



    private void Awake()
    {

        _config = GetComponent<CharacterConfig>();

        _playerControls = new PlayerControls();


    }
    private void Start()
    {
        // Setting up atributes
        if (_config != null)
        {
            _maxCombo = _config.MaxCombo;
            _attackDamage = _config.AttackDamage;
            _attackRange = _config.AttackRange;
            _dashPower = _config.DashPower;
            HealthPoints = _config.TotalHealthPoints;
        }
        _animator = GetComponent<Animator>();
        _rb2D = GetComponent<Rigidbody2D>();
        _enemy = gameObject.GetComponent<EnemyReference>();


        // Set up input controller
        if (gameObject.name == "Player1")
        {
            // Move input and callbacks
            _playerControls.Gameplay.Move.performed += ctx => CheckMove(ctx);
            _playerControls.Gameplay.Move.canceled += ctx => _move = Vector2.zero;

            // Dash input and callbacks
            _playerControls.Gameplay.DashForward.performed += ctx => CheckDashForward();
            _playerControls.Gameplay.DashBackward.performed += ctx => CheckDashBackward();

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

            // Jump input
            _playerControls.Gameplay2.Jump.performed += ctx => CheckJump();

            // Attack input
            _playerControls.Gameplay2.Attack.performed += ctx => CheckAttack();
        }
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
        if (_enemy.Enemy.transform.position.x < gameObject.transform.position.x)
        {

            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            _righttoLeft = false;
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            _righttoLeft = true;

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

    private void CheckDashForward()
    {
        if (!_animator.GetBool("Dash") && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1Animation"))
        {
            StartCoroutine(ActionDash(1));
        }
    }
    private void CheckDashBackward()
    {
        if (!_animator.GetBool("Dash") && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1Animation"))
        {
            StartCoroutine(ActionDash(-1));
        }
    }

    private IEnumerator ActionDash(int direction)// Move player on X axis when the input si higher than 0 and actives/stop the animation
    {
        _animator.SetBool("Dash", true);
        _rb2D.velocity = new Vector2(_dashPower * direction, 0f);
        yield return new WaitForSeconds(_dashingTime);
        _rb2D.velocity = new Vector2(0f, 0f);
        _animator.SetBool("Dash", false);
        yield return new WaitForSeconds(_dashingCooldown);

    }

    private void DashEnd()
    {
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
                targetHit.TakeHit(_attackDamage);
            }
        }
    }

    private void FireProyectile()
    {
        GameObject _proyectile = Instantiate(_config.Proyectile, _shootingPoint.transform.position, _shootingPoint.transform.rotation);
        _proyectile.GetComponent<ProyectilManager>().setDirection(_righttoLeft);
    }


    public void TakeHit(int damage) // Decrese the health points when hitted
    {
        if (HealthPoints <= 0)
        {
            _animator.SetBool("IsHited", false);
            return;
        }
        if (!_animator.GetBool("IsHited"))
        {
            _animator.SetBool("IsHited", true);
        }

        HealthPoints -= damage;

        GameManager.Instance.ChangeHealth(HealthPoints, gameObject.name);

        _animator.SetInteger("HP", HealthPoints);
        if (HealthPoints <= 0)
        {
            if (gameObject.name == "Player1")
            {
                GameManager.Instance.SetWinner("Player 2");
            }
            else
            {
                GameManager.Instance.SetWinner("Player 1");
            }


            _animator.SetBool("IsHited", false);
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
