using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private PlayerControls _playerControls;
    private Rigidbody2D rb2D;
    [Header("Movement")]
    [SerializeField]
    private float speed;
    private float dirX;

    [Header("Jump")]
    [SerializeField]
    private float JumpForce;

    private bool CanAttackAgain = false;
    private bool Attack = false;
    private bool Jump = false;


    [Header("Animation")]

    private Animator animator;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerControls.Gameplay.Move.performed += ctx => ActionMove();
        _playerControls.Gameplay.Jump.performed += ctx => ActionMove();
    }

    private void OnEnable()
    {
        _playerControls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Gameplay.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis("Horizontal") * speed;//Toma la direcci�n de movimiento y le da velocidad.
        animator.SetFloat("Speed", Mathf.Abs(dirX));//Envia al animator la velocidad del pnje.



        if (Input.GetButtonDown("Jump") && animator.GetBool("IsJumping") == false) //Detecta salto
        {
            animator.SetBool(("IsJumping"), true);
            Jump = true;

        }

        if (Input.GetButtonDown("Fire1"))//detecta atacar.
        {
            Attack = true;

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1Animation"))
            {
                // El jugador ha presionado el bot�n de ataque mientras se estaba reproduciendo la animaci�n del primer ataque
                CanAttackAgain = true;
            }



        }



    }

    private void FixedUpdate()
    {

        if (Attack)
        {
            animator.SetBool("IsAttacking", true); //Inicia animaci�n de ataque
            animator.SetInteger("ComboAttack", 1);


            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) // Verifica si la animaci�n ya acabo.
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1Animation") && CanAttackAgain) // Verifica si se puede realizar un segundo ataque.
                {
                    Debug.Log("Can attack");
                    animator.SetInteger("ComboAttack", 2); //Inicia un segundo ataque.
                    CanAttackAgain = false; // No se permite hacer otro ataque hasta que la animaci�n termine.
                }
                else
                {
                    Attack = false;
                    animator.SetBool("IsAttacking", false);
                    animator.SetInteger("ComboAttack", 0);
                }
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2Animation"))
                {
                    animator.SetBool("IsAttacking", false);  // desactiva el par�metro de ataque 1 en el animator
                    animator.SetInteger("ComboAttack", 0);  // desactiva el par�metro de ataque 2 en el animator
                    Attack = false;
                    CanAttackAgain = false;
                }
            }
        }
        animator.SetFloat("JumpVelocity", rb2D.velocity.y);//envia al animator la velocidad en componente y para transicionar.
        if (Jump)
        {
            ActionJump();//SALTA!
        }
        if (dirX != 0)
        {
            ActionMove(); //Muevete!
        }



    }
    private void ActionJump()//Realiza el salto
    {


        Jump = false;
        rb2D.AddForce(new Vector2(0f, JumpForce));//Agregamos una fuerza vertical

    }
    private void ActionMove()//Actualiza la posici�n del personaje.
    {
        transform.position += new Vector3(dirX, 0, 0) * Time.fixedDeltaTime;
    }


    private void OnCollisionEnter2D(Collision2D other) //Detecta la colision con el suelo
    {
        if (other.collider.CompareTag("Floor"))
        {
            animator.SetBool("IsJumping", false);//desactiva el salto, para poder pasar a otra animaci�n.
        }
    }



}
