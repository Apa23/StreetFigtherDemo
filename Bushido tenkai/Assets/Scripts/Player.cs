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
    private bool _canAttack = false;

    // Attack point reference
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;

    // Input Interface attributes
    private PlayerControls _playerControls;
    private Vector2 _move;

    // Animator component
    [Header("Animation")]
    private Animator _animator;

    // Rigidbody component
    private Rigidbody2D _rb2D;


    private void Awake()
    {
        // Set up input controller
        _playerControls = new PlayerControls();

        // Move input and callbacks
        _playerControls.Gameplay.Move.performed += ctx => CheckMove(ctx);
        _playerControls.Gameplay.Move.canceled += ctx => _move = Vector2.zero;

        // Jump input
        _playerControls.Gameplay.Jump.performed += ctx => CheckJump();

        // Attack input
        _playerControls.Gameplay.Attack.performed += ctx => CheckAttack();
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

        if (_canAttack)
        {

            _canAttack = false;
            _animator.SetBool("IsAttacking", false);
        }

    }

    private void OnEnable() //Enable input controls
    {
        _playerControls.Gameplay.Enable();
    }

    private void OnDisable() //Disable input controls
    {
        _playerControls.Gameplay.Disable();
    }

    private void CheckAttack() //Detect attack and active the animation
    {
        if (_animator.GetBool("IsAttacking") == false)
        {
            _move = Vector2.zero;
            _canAttack = true;
            _animator.SetBool("IsAttacking", true);
            _animator.SetInteger("ComboAttack", 1);
            Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider2D enemy in hitEnemys)
            {
                if (enemy.transform.TryGetComponent(out IDamageable targetHit))
                {
                    targetHit.TakeHit();
                }
            }
        }


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

    private void CheckMove(InputAction.CallbackContext ctx)
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1Animation"))
        {
            _move = ctx.ReadValue<Vector2>();
        }

    } //Detect

    private void ActionMove()// Move player on X axis when the input si higher than 0 and actives/stop the animation
    {
        Vector2 dir = new Vector2(_move.x * 2, 0) * Time.deltaTime;
        transform.Translate(dir, Space.World);
        _animator.SetFloat("Speed", Mathf.Abs(dir.x));
    }

    public void TakeHit() // Decrese the health points when hitted
    {

        if (HealthPoints <= 0)
        {
            return;
        }

        _animator.SetBool("IsHited", true);
        Debug.Log("hit");
        HealthPoints--;
        _animator.SetInteger("HP", HealthPoints);
        _animator.SetBool("IsHited", false);

        if (HealthPoints <= 0)
        {
            _playerControls.Gameplay.Disable();
            return;
        }

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
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
