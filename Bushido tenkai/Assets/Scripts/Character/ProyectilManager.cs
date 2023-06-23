using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilManager : MonoBehaviour
{

     private void OnCollisionEnter2D(Collision2D other) //Detect collision
    {
        if (other.collider.CompareTag("Player")) // When the player collides with the floor
        {
            other.gameObject.GetComponent<PlayerManager>().TakeHit(20);
        }
    }
}
