using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSistem : MonoBehaviour
{
    private Animator animator;
    private bool atacar;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }
    private void FixedUpdate()
    {
        
    }
    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {   if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1Animation"))
            {

                atacar = true;
                
            }
            else
            {
                animator.SetBool("IsAttacking",true);
       
            }

        }
        if ( atacar)
        {
            animator.SetBool("DoubleAttack", true);


        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1Animation") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && atacar)
        {
            animator.SetBool("DoubleAttack", true);
            animator.SetBool("IsAttacking", false);
        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1Animation") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            animator.SetBool("IsAttacking", false);
        }
            
        
        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2Animation") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)) {
            animator.SetBool("DoubleAttack", false);
            atacar = false;
        }
    }

}

