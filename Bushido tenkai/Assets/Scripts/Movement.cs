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
    [SerializeField]
    private LayerMask Floor;
    [SerializeField]
    private Transform FloorController;
    [SerializeField]
    private Vector3 BoxDimensions;//Una caja que nos permitira saber cuando estemos en el piso.
    [SerializeField]
    private bool OnFloor;
    
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

    }

    private void FixedUpdate()
    {
        OnFloor = Physics2D.OverlapBox(FloorController.position, BoxDimensions, 0f, Floor);//Estamos en suelo mientras la caja toque suelo
        animator.SetBool("OnFloor", OnFloor);//Envia al animator si el pnje esta en suelo o no.
        animator.SetFloat("JumpVelocity", rb2D.velocity.y);//envia al animator la velocidad en componente y para transicionar.
        if (OnFloor && Jump)
        {
            ActionJump();
        }
        if (dirX != 0)
        {
            ActionMove();
        }
        
    }
    private void ActionJump()//Realiza el salto
    {
        
        OnFloor = false;
        Jump = false;
        rb2D.AddForce(new Vector2(0f, JumpForce));//Agregamos una fuerza vertical

    }
    private void ActionMove()//Actualiza la posición del personaje.
    {
        transform.position += new Vector3(dirX, 0, 0) * Time.fixedDeltaTime;
    }

    private void OnDrawGizmos()//Nos permite ver la caja que controla si estamos en suelo o no.
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(FloorController.position, BoxDimensions);
    }

}
      