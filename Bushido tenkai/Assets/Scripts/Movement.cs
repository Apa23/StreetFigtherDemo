using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{   private Rigidbody2D rb2D;
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


    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis("Horizontal") * speed;//Toma la dirección de movimiento y le da velocidad.
        animator.SetFloat("Speed",Mathf.Abs(dirX));//Envia al animator la velocidad del pnje.

        

        if (Input.GetButtonDown("Jump") && animator.GetBool("IsJumping")==false ) //Detecta salto
        {
            animator.SetBool(("IsJumping"),true);
            Jump = true;

        }

        if (Input.GetButtonDown("Fire1"))//detecta atacar.
        {
            Attack = true;
            
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1Animation"))
            {
                // El jugador ha presionado el botón de ataque mientras se estaba reproduciendo la animación del primer ataque
                CanAttackAgain = true;
            }
            


        }
            


    }

    private void FixedUpdate()
    {

        if (Attack)
        {
            animator.SetBool("IsAttacking", true); //Inicia animación de ataque

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) // Verifica si la animación ya acabo.
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1Animation") && CanAttackAgain) // Verifica si se puede realizar un segundo ataque.
                {
                    animator.SetBool("DobleAttack", true); //Inicia un segundo ataque.
                    CanAttackAgain = false; // No se permite hacer otro ataque hasta que la animación termine.
                }
                else
                {
                    Attack = false;
                    animator.SetBool("IsAttacking", false);
                }
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2Animation"))
                {
                    animator.SetBool("IsAttacking", false);  // desactiva el parámetro de ataque 1 en el animator
                    animator.SetBool("DobleAttack", false);  // desactiva el parámetro de ataque 2 en el animator
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
    private void ActionMove()//Actualiza la posición del personaje.
    {
        transform.position += new Vector3(dirX, 0, 0) * Time.fixedDeltaTime;
    }


    private void OnCollisionEnter2D(Collision2D other) //Detecta la colision con el suelo
    {
        if (other.collider.CompareTag("Floor"))
        {
            animator.SetBool("IsJumping", false);//desactiva el salto, para poder pasar a otra animación.
        }
    }

 

}
      